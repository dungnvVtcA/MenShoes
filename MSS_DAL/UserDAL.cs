using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Threading.Tasks;

namespace MSS_DAL
{
    public class UserDAL
    {
        private MySqlConnection connection;
        public UserDAL()
        {
            connection = DBHelper.OpenConnection();

        }
        public User Login(string id , string pass)
        {
            string query = "Select User_id,User_Type from Users where AccountName='"+ id +"' and User_Password ='"+pass+"';";
            if( connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query,connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            User u = null;
            if(reader.Read())
            {  
                u = new User();
                
                u.User_id = reader.GetInt32("User_id");
                u.Type  =  reader.GetInt32("User_Type");  
                return u;
               
            }
            reader.Close();
            connection.Close();
            return u;
        }
       
    }
    
}
