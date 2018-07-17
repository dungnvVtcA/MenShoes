using System ;
using Xunit;
using MSS_DAL;
using MSS_Persistence;
using System.Collections.Generic;

namespace DAL.test
{
    public class ShoesTest
    {
        ShoesDAL shdal = new ShoesDAL();
        [Fact]
        public void GetAllShoes_test()
        {
            List<Shoes> listShoes = shdal.GetAllShoes();
            Assert.NotNull(listShoes);
        }
        [Fact]
        public void GetShoesById_Test()
        {
            Shoes sh = shdal.GetShoesById(1);
            Assert.NotNull(sh);                                                                             
        }
         [Fact]
        public void GetShoesById_TestNULL()
        {
            Shoes sh = shdal.GetShoesById(100);
            Assert.Null(sh);                                                                             
        }



    }
}