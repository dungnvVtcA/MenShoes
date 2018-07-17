using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace MSS_DAL
{
    
    public class DBHelper
    {
        private static string CONNECTION_STRING = "server=localhost;user id=root;pwd=12345678;port=3306;database=MenShoes;Sslmode=none";
        public static MySqlConnection OpenDefaultConnection()
        {
            try{
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = CONNECTION_STRING
                };
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }

        public static MySqlConnection OpenConnection()
        {
            try
            {
                if (CONNECTION_STRING == null)
                {
                    using (FileStream fileStream = File.OpenRead("ConnectionString.txt"))
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            CONNECTION_STRING = reader.ReadLine();
                        }
                    }
                }
                return OpenConnection(CONNECTION_STRING);
            }
            catch
            {
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try{
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }
    }
}