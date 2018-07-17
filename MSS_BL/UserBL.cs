using System;
using System.Collections.Generic;
using MSS_Persistence;
using MSS_DAL;

namespace MSS_Bl
{
    public class UserBl
    {
        UserDAL u = new UserDAL();
        public User login(string id , string pass)
        {
            return u.Login(id,pass);
        }
    
    }
}
