using System;
using System.Collections.Generic;
using MSS_Persistence;
using MSS_DAL;
namespace MSS_Bl
{
    public class ShoesBL
    {
        ShoesDAL shoes = new ShoesDAL();
        public Shoes GetShoesById(int Shoes_id)
        {
            return shoes.GetShoesById(Shoes_id);
        }
        public List<Shoes> GetAllShoes()
        {
            return shoes.GetAllShoes();
        }
    }
}