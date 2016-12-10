using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace guitarTools.Classes
{
    // TODO Write documentation for SQLCommands class
    /// <summary>
    /// 
    /// </summary>

    class SQLCommands
    {
        #region Property definition
        public static SqlConnection SqlConn { get; set; }
        public static SqlCommand SqlComm { get; set; }
        public static SqlDataReader SqlRead;
        #endregion

        public bool CheckConnection()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Data.mdf';Integrated Security=True;Connect Timeout=30");
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        private static void Connect(string command)
        {
            SqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Data.mdf';Integrated Security=True;Connect Timeout=30");
            SqlComm = new SqlCommand();
            SqlConn.Open();
            SqlComm.Connection = SqlConn;
            SqlComm.CommandText = command;
            SqlRead = SqlComm.ExecuteReader();
        }

        public static List<T> FetchList<T>(string command)
        {
            Connect(command);

            // Defining generic list
            List<T> data = new List<T>();

            // Executes only if table has content
            #region Data extraction
            if (SqlRead.HasRows)
            {
                while (SqlRead.Read())
                {
                    data.Add((T)Convert.ChangeType(SqlRead[0], typeof(T))); // Converts to selected generic type and appends to list
                }
            }
            #endregion

            // Closing SQL connection
            SqlConn.Close();

            return data; // Returning fetched data
        }

        public static T FetchData<T>(string command)
        {
            Connect(command);
          
            return (T)Convert.ChangeType(SqlRead[0], typeof(T));
        }
    }
}