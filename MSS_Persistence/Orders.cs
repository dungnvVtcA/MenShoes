using System;
using System.Collections.Generic;

namespace MSS_Persistence
{

    public class Orders
    {
        public int Order_id{set;get;}

        public DateTime Date_Order{set;get;}

        public User user{set; get;}

        public int Order_status{set;get;}

        private List<Shoes> ShoesList;
        public List<Shoes> shoesList { get => ShoesList; set => ShoesList = value; }

        public Orders(){
            user = new User();
            shoesList = new List<Shoes>();
        }

        
    }
}