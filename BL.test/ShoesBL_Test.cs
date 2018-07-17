using System;
using Xunit;
using MSS_Bl;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Collections.Generic;


namespace BL.test
{
    public class ShoesBL_Test
    {
        ShoesBL sbl = new ShoesBL();
        
        
        [Fact]
        public void GetAllShoes_test()
        {
            List<Shoes> listShoes = sbl.GetAllShoes();
            Assert.NotNull(listShoes);
        }
        [Theory]

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]

        public void GetShoesById_Test(int id)
        {
            Shoes sh = sbl.GetShoesById(id);
            Assert.NotNull(sh); 
        }
        [Fact]
        public void GetShoesById_TestNULL()
        {
            Shoes sh = sbl.GetShoesById(100);
            Assert.Null(sh);                                                                             
        }

    }
}