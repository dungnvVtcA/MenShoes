using System;
using Xunit;
using MSS_DAL;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Collections.Generic;

namespace DAL.test
{
    public class OrderTest
    {
        OrdersDAl odl = new OrdersDAl();

        [Fact]
        public void Test_GetALLOrders()
        {
            var result = odl.GetOrders(0,null);
            Assert.NotNull(result);
        }
        [Theory]

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void Test_GetALLOrdersByIDUser(int id)
        {
            var result = odl.GetOrders(1,new Orders{user = new User{User_id=id}});
            Assert.NotNull(result);
        }
        [Theory]

        [InlineData(0)]
        [InlineData(1)]

        public void Test_GetAllOrderbyStatus(int status)
        {
            var result = odl.GetOrders(2,new Orders{Order_status = status});
            Assert.NotNull(result);
        }
        [Fact]

        public void Test_update_True()
        {
            Assert.True(odl.Update(1));
        }

        [Fact]

        public void GetOrderDetailsByID()
        {
            Assert.NotNull(odl.GetOrderDetailsByID(1));
        }

        [Fact]

        public void Create_test()
        {
            Orders or = new Orders();
            or.user = new User();
            or.Order_id = 1;
            or.Order_status = 1;
            or.user.User_id = 1;
            or.shoesList = new List<Shoes>();
            Shoes sh = new Shoes();
            sh.Shoes_id = 1;
            sh.Amount = 2;
            or.shoesList.Add(sh);
            Assert.True(odl.CreateOrders(or));
        }

    }
}