using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace guitarTools.Classes
{
    // TODO Write documentation for SQLCommands class

    class SQLCommands
    {
        public static SqlConnection SqlConn { get; set; }
        public static SqlCommand SqlComm { get; set; }
        public static SqlDataReader SqlRead;

        public bool CheckConnection()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\hp\Documents\Projects\Visual Studio\C#\guitarToolsForm\guitarTools\SQL\Data.mdf';Integrated Security=True;Connect Timeout=30");
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        private static void Connect()
        {
            // TODO Set relative path for SQL Connection
            SqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\hp\Documents\Projects\Visual Studio\C#\guitarToolsForm\guitarTools\SQL\Data.mdf';Integrated Security=True;Connect Timeout=30");
            SqlComm = new SqlCommand();
        }

        public static List<T> FetchList<T>(string command, ushort columnCount)
        {
            Connect();

            // Opening SQL connection and setting up  
            #region SQL Setup
            SqlConn.Open();
            SqlComm.Connection = SqlConn;
            SqlComm.CommandText = command;
            SqlRead = SqlComm.ExecuteReader();
            #endregion

            // Defining generic list
            List<T> data = new List<T>();

            // Executes only if table has content
            #region Data extraction
            if (SqlRead.HasRows)
            {
                while (SqlRead.Read())
                {
                    for (int column = 0; column < columnCount; column++)
                        data.Add((T)Convert.ChangeType(SqlRead[column], typeof(T))); // Converts to selected generic type and appends to list
                }
            }
            #endregion

            // Closing SQL connection
            SqlConn.Close();

            return data;
        }

        // TODO Write logic for method
        public static T FetchData<T>(string command, ushort columnCount)
        {
            Connect();

            return (T)Convert.ChangeType(5, typeof(int));
        }
    }
}
