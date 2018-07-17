using System;
using MSS_Bl;
using MSS_DAL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Text;
using System.Security;
using System.Text.RegularExpressions;
namespace MSS_Console
{
    public class MenuStaff
    {
        
        public static void Menustaff()
        {
            OrderBL obl = new OrderBL();
            int b = 0;
            var result = obl.GetAllOrder();
            foreach (var or in result)
            {
                if(or.Order_status == 1)
                {
                    b++;
                }
            }
            while(true)
            {
                Console.Clear();
                string line = "===================================\n";
                Console.Write(line);
                Console.Write("Men Shoes\n");
                Console.Write(line);
                Console.WriteLine("1.Browse orders({0})",b);
                Console.WriteLine("2.The list of approved orders");
                Console.WriteLine("3.Reload page");
                Console.WriteLine("4.Log Out");
                Console.Write(line);
                Console.Write("You Choose :");
                var choice = Console.ReadLine();
                if(choice == " ")
                {
                    Console.ReadLine();
                }
                switch(choice)
                {
                    case "1" : MenuStaff.BrowseOrders();
                    break;
                    case "2" :MenuStaff.ListApprovedorders();
                    break;
                    case "3" :MenuStaff.Reload();
                    break;
                    case "4" :
                    MENU m = new MENU();
                    m.MainMENU();
                    break;
                    default :
                    break;
                }
                
                
            }
        }
        public static bool BrowseOrders()
        {
            Console.Clear();
            string line1 = "------------------------------------------------------------------------------------\n";
            Decimal a = 0;
            OrderBL obl = new OrderBL();
            var list = obl.GetAllOrderbyStatus(1);
            if(list.Count!= 0)
            {
                    string line = "============================================================================================";
                    Console.WriteLine(line);
                    Console.WriteLine(" Order_id        |   User_ID        |  Date                         |   Order Status  |");
                    Console.WriteLine(line);
                    foreach (var orders in list)
                    {
                            Console.WriteLine("{0,-20} {1,-20} {2,-29} {3}",orders.Order_id,orders.user.User_id,orders.Date_Order,orders.Order_status);
                        
                        
                    }
                    Console.WriteLine(line);
                    Console.Write("Do you want to view customer order details? (y/n):");
                    char choicce = Convert.ToChar(Console.ReadLine());
                    if(choicce == 'y')
                    {
                        while(true){
                        Console.Write("Enter the ID to view the customer's order details: ");
                            while (true)
                            {
                                
                                try
                                {
                                    
                                    int or_id = Convert.ToInt32(Console.ReadLine());
                                    var orderdetail = obl.GetOrderDetailsByID(or_id);
                                    if(orderdetail != null && orderdetail.Order_status == 0 )
                                    {
                                        throw new valueexception("Order  does not exist or has been approved,please re-enter : ");
                                    }
                                    else if(orderdetail != null && orderdetail.Order_status == 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("          Order Detail of Customer Information        ");
                                        Console.WriteLine("-ID                : {0}",orderdetail.Order_id);
                                        Console.WriteLine("-Order date        : {0}",orderdetail.Date_Order);
                                        Console.WriteLine("-Customer name     : {0}",orderdetail.user.User_name );
                                        Console.WriteLine("-Customer phone    : {0}",orderdetail.user.Phone);
                                        Console.WriteLine("-Customer Address  : {0}",orderdetail.user.Address);
                                        Console.WriteLine("-Customer Email    : {0}",orderdetail.user.Email);
                                        Console.Write(line1);
                                        Console.Write(" Product name      |  Unitprice          |  Amount          |  Size     |     Total          \n");
                                        foreach (var order in orderdetail.shoesList)
                                        {
                                            Decimal b = order.Price*order.Amount;
                                            Console.WriteLine("{0,-20} {1,-26} {2,-15} {3,-10} {4}",order.Shoes_name,order.Price,order.Amount,order.Size,b);
                                            a +=b;
                                        
                                        }
                                        Console.Write(line1);
                                        Console.WriteLine("-The total amount payable                                                  {0}",a);
                                        Console.WriteLine("Do you want to Browse this Order?(y/n)");
                                        string choice3 = Console.ReadLine();
                                        if( choice3 =="y")
                                        {
                                            obl.update(or_id);
                                            Console.WriteLine("...Browse  Order successful!");
                                            
                                        }
                                    }
                                }
                                catch(valueexception e)
                                {
                                    Console.Write(e.Message);
                                    continue;
                                }
                                catch
                                {
                                    Console.Write("Please enter a valid value :");
                                    continue;
                                }
                                break;
                            }
                            
                            Console.Write("You want to browse order?(y/n)");
                            string choice = Console.ReadLine();
                            if( choice == "n")
                            {
                                break;
                                
                            }else if( choice == "y")
                            {
                                BrowseOrders();
                                break;
                            }
                        
                        }
                    }
                    
                return true;
            }
            else if(list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The orders has been processed!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadLine();
            }
            return true;

        }

        public static void ListApprovedorders()
        
        {
            Console.Clear();
            OrderBL obl = new OrderBL();
            var list = obl.GetAllOrderbyStatus(0);
            if( list.Count != 0)
            {
                 string line = "============================================================================================";
                Console.WriteLine(line);
                Console.WriteLine(" Order_id        |   User_ID        |            Date                  |   Order Status  |");
                Console.WriteLine(line);
                foreach (var orders in list)
                {
                    if( orders.Order_status == 0)
                    {
                        Console.WriteLine("{0,-20} {1,-20} {2,-33} {3}",orders.Order_id,orders.user.User_id,orders.Date_Order,orders.Order_status);
                    }
                            
                }
                Console.WriteLine(line);
                Console.Write("\n    Press Enter key to back menu... !");
                Console.ReadLine();
            }
            else if(list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty list, no invoices processed...!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadLine();

            }
            
        }

        public static void Reload()
        {
            Menustaff();
        }

    }
}