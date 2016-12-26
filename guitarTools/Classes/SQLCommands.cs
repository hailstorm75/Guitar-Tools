using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace guitarTools.Classes
{
    /// <summary>
    /// A set of instructions which fetch data from the 
    /// database and output them to a variable or list
    /// </summary>

    class SQLCommands
    {
        #region Property definition
        public static SqlConnection SqlConn { get; set; }
        public static SqlCommand SqlComm { get; set; }
        public static SqlDataReader SqlRead;
        #endregion

        public static bool CheckConnection()
        {
            // Returns a boolean value based on connection success/failure
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
            // Defines connection to database
            SqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Data.mdf';Integrated Security=True;Connect Timeout=30");
            SqlComm = new SqlCommand();
        
            SqlConn.Open();                 // Opens the connection
            SqlComm.Connection = SqlConn;
            
            SqlComm.CommandText = command;  // Passes the command
            SqlRead = SqlComm.ExecuteReader();
        }

        public static List<T> FetchList<T>(string command)
        {
            Connect(command); // Connects to database and sends command

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
          
            SqlConn.Close(); // Closing SQL connection

            return data; // Returns fetched data
        }

        public static T FetchData<T>(string command)
        {
            Connect(command); // Connects to database and sends command

            SqlConn.Close(); // Closing SQL connection

            return (T)Convert.ChangeType(SqlRead[0], typeof(T)); // Returns fetched  data
        }
    }
}