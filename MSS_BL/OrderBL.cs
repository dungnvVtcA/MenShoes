using System;
using System.Collections.Generic;
using MSS_Persistence;
using MSS_DAL;
namespace MSS_Bl
{
    public class OrderBL
    {
        OrdersDAl or = new OrdersDAl();
        public bool CreateOrder(Orders orders)
        {
            bool create = or.CreateOrders(orders);
            return create;
        }
        public bool InsertShoppingCarrt(int Order_id, int Shoes_id, int Amount)
        {
            return or.InsertShoppingCarrt(Order_id,Shoes_id,Amount);
        }
        public bool AddShppingCart(Orders orders)
        {
            bool create = or.AddShoppingCart(orders);
            return create;
        }
        public List<Orders> GetAllOrder()
        {
            return or.GetOrders( 0,null);
        }
        public List<Orders> GetAllOrderbyStatus(int status)
        {
            return or.GetOrders(2, new Orders{Order_status = status});
        }
        public bool update(int id)
        {
            return or.Update(id);
        }
        public List<Orders> GetAllOrderByIDUser(int user_id)
        {
            return or.GetOrders(1, new Orders{user = new User{User_id=user_id}});
        }
        public Orders GetOrderDetailsByID(int id)
        {
            return or.GetOrderDetailsByID(id);
        }
        public bool updateAmount( int amount , int order_id)
        {
            return or.UpdateAmout(amount, order_id);
        }
        public bool Delete(int amount, int order_id )
        {
            return or.Delete(amount, order_id);
        }
        public bool DeleteShoppingCart(int status, int order_id )
        {
            return or.DeleteShoppingCart(status,order_id);
        }
    }
}