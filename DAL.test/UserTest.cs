using System;
using Xunit;
using MSS_Persistence;
using MSS_DAL;
using MySql.Data.MySqlClient;

namespace DAL.test
{
    public class UserTest
    {
        UserDAL u = new UserDAL();
        [Fact]
        public void LogIN_test()
        {
            User user = u.Login("Nguyenvana","A12345678");
            
            Assert.NotNull(user);
        }
        [Theory]

        [InlineData("Nguyenvandung","dung1234567800")]
        [InlineData("Nguyenvanahh","A12345678")]
        [InlineData("Nguyenvanahh","A123456783")]

        public void LogIN_testnull(string id ,string pass)
        {
            User user = u.Login(id,pass);
            
            Assert.Null(user);
        }
    }
}
