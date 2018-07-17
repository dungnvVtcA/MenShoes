using System;
using Xunit;
using MSS_Bl;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Collections.Generic;


namespace BL.test
{
    public class UserBL_Test
    {
        UserBl u = new UserBl();
        [Fact]
        public void Login_TestNOTNULL()
        {
            Assert.NotNull(u.login("Nguyenvana","A12345678"));
            
        }

        [Theory]

        [InlineData("Nguyenvandung","dung1234567800")]
        [InlineData("Nguyenvanahh","A12345678")]
        [InlineData("Nguyenvanahh","A123456783")]

        public void LogIN_testnull(string id ,string pass)
        {
            User user = u.login(id,pass);
            
            Assert.Null(user);
        }
    }
    
}
