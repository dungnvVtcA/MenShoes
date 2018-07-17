using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MSS_Persistence;
namespace MSS_DAL
{
    public class ShoesDAL
    {
        private MySqlConnection connection;

        private MySqlDataReader reader;
        public ShoesDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public List<Shoes> GetAllShoes()
        {
            List<Shoes> sh1 = new List<Shoes>();
            string  query = "Select Shoes.Shoes_id ,Shoes.Shoes_name,Trademark.TM_id,Trademark.TM_name,Trademark.Origin,Shoes.Color,Shoes.Material,Shoes.Price,Shoes.Size,Shoes.Manufacturers,Shoes.Style,Shoes.Amount from Shoes inner join Trademark on Shoes.TM_id = Trademark.TM_id ;";
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using(reader = cmd.ExecuteReader())
                {
                
                    while(reader.Read())
                    {
                        sh1.Add(GetShoes(reader));
                    }
                    reader.Close();
                }
            connection.Close();
            return sh1;
        }

        private static Shoes GetShoes(MySqlDataReader reader)
        {
            Shoes sh = new Shoes();
                sh.Shoes_id = reader.GetInt32("Shoes_id");
                sh.Shoes_name = reader.GetString("Shoes_name");
                sh.TM =  new Trademark();
                sh.TM.Trademark_id = reader.GetInt16("TM_id");
                sh.TM.Name = reader.GetString("TM_name");
                sh.TM.Origin = reader.GetString("Origin");
                sh.Color = reader.GetString("Color");
                sh.Material = reader.GetString("Material");
                sh.Price = reader.GetDecimal("Price");
                sh.Size = reader.GetInt32("Size");
                sh.Manufacture = reader.GetString("Manufacturers");
                sh.Style = reader.GetString("Style");
                sh.Amount = reader.GetInt32("Amount");
                return sh;
        }
        public Shoes GetShoesById(int Shoes_id)
        {
             if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            Shoes sh = null;
            string  query = "Select Shoes.Shoes_id ,Shoes.Shoes_name,Trademark.TM_id,Trademark.TM_name,Trademark.Origin,Shoes.Color,Shoes.Material,Shoes.Price,Shoes.Size,Shoes.Manufacturers,Shoes.Style,Shoes.Amount from Shoes inner join Trademark on Shoes.TM_id = Trademark.TM_id where Shoes.Shoes_id='"+Shoes_id+"';";
           
            
            MySqlCommand cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();
                
            if(reader.Read())
            {
                sh= new Shoes();
                sh = GetShoes(reader);
            }
            
            connection.Close();
            return sh;

        }
    }
}