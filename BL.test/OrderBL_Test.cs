using System;
using Xunit;
using MSS_Bl;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Collections.Generic;
namespace BL.test
{
    public class OrderBL_Test
    {
        OrderBL obl = new OrderBL();
        [Fact]
        public void Test_GetALLOrders()
        {
            var result = obl.GetAllOrder();
            Assert.NotNull(result);
        }
        [Theory]

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void Test_GetALLOrdersByIDUser(int id)
        {
            var result = obl.GetAllOrderByIDUser(id);
            Assert.NotNull(result);
        }
        [Theory]

        [InlineData(0)]
        [InlineData(1)]

        public void Test_GetAllOrderbyStatus(int status)
        {
            var result = obl.GetAllOrderbyStatus(status);
            Assert.NotNull(result);
        }

        [Fact]

        public void Test_update_True()
        {
            Assert.True(obl.update(1));
        }
        [Fact]

        public void GetOrderDetailsByID()
        {
            Assert.NotNull(obl.GetOrderDetailsByID(1));
        }
        [Fact]

        public void Create_test()
        {
           Assert.False(obl.CreateOrder( new Orders()));
        }

    }
}