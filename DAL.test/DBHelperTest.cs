using System;
using Xunit;
using MySql.Data.MySqlClient;
using MSS_DAL;

namespace DAL.test
{
    public class DBHelperTest
    {
        [Fact]
        public void OpenConnectionTest()
        {
            Assert.NotNull(DBHelper.OpenConnection());
        }
        
         [Fact]
        public void OpenDefaultConnectionTest()
        {
            Assert.NotNull(DBHelper.OpenDefaultConnection());
        }
        [Theory]
        [InlineData("server=localhost1;user id=vtca;password=vtcacademy;port=3306;database=OrderDB;SslMode=None")]
        [InlineData("server=localhost;user id=vtca231;password=vtcacademy;port=3306;database=OrderDB;SslMode=None")]
        [InlineData("server=localhost;user id=vtca;password=vtcacademy34242;port=3306;database=OrderDB;SslMode=None")]
        [InlineData("server=localhost;user id=vtca;password=vtcacademy;port=3307;database=OrderDB;SslMode=None")]
        [InlineData("server=localhost;user id=vtca;password=vtcacademy;port=3306;database=OrderDB1234;SslMode=None")]
        [InlineData("server=localhost;user id=vtca;password=vtcacademy;port=3306;database=OrderDB;SslMode=Non")]
        [InlineData("server=localhost;user id=vtca;password=vtcacademy;port=3306;database=OrderDB")]
        public void OpenConnectionWithStringFailTest(string connectionString)
        {
            Assert.Null(DBHelper.OpenConnection(connectionString));
        }
        
    }
}
