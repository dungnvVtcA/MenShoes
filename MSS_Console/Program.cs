using System;
using MSS_Bl;
using MSS_DAL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MSS_Persistence;
using System.Text;
using System.Security;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MSS_Console
{
    class Program {
    static void Main(String[] arg)
    {
        MENU menu = new MENU();
        menu.MainMENU();
    }
    
    }
    class valueexception: Exception
        {
            public valueexception(string massage): base(massage)
            {
            }
        }
    class MENU
    {
        User result = new User();
        UserBl ubl =new UserBl();
        Shoes shoes = new Shoes();

        OrderBL obl = new OrderBL();
        ShoesBL sbl = new ShoesBL();
        private static int id , sh_id , index = 0,oredr_iddelete;

        private static bool validate(string str)
        {
                Regex regex = new Regex("[a-zA-Z0-9_]");
                MatchCollection matchCollectionstr = regex.Matches(str);
                if (matchCollectionstr.Count < str.Length)
                {
                    return false;
                }
                return true;
        }
        private static short Menu(string title, string[] menu)
        {
            short choose = 0;
            string line = "========================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menu[i]);
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            }while (choose <= 0 || choose > menu.Length);
            return choose;
        }
        private static short Menu2(string title, string[] menu)
        {
            short choose = 0;
            Console.WriteLine(" " + title);
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menu[i]);
            }
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            }while (choose <= 0 || choose > menu.Length);
            return choose;
        }
        public  void MainMENU()
        {
            while(true)
            {
                Console.Clear();
                string line = "===================================================\n";
                Console.Write(line);
                Console.WriteLine("        MEN SHOES STORE   ");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1 . Log in.");
                Console.WriteLine("2 . Exit");
                Console.Write(line);
                Console.Write("#You Choose :");
                var choose = Console.ReadLine();
                if(choose == " ")
                {
                    Console.ReadLine();
                }
                switch (choose)
                {
                    case "1" : 
                    Console.Clear();
                    while(true)
                    {
                        
                        Console.Write("---------      LOG IN      ---------\n");
                        Console.Write("-Username :");
                        string name = Console.ReadLine();
                        Console.Write("-Password : ");
                        string pass = GetConsolePassword();
                        while ((validate(name) == false) || (validate(pass) == false))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(".  .  .Username or password wrong, please re-enter ! ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("You want to keep signing in?(y/n) :");
                            string chon = Console.ReadLine();
                            if(chon == "n")
                            {
                                MainMENU();
                            }
                            Console.Clear();
                            Console.Write("---------      LOG IN      ---------\n");
                            Console.Write("-Username: ");
                            name = Console.ReadLine();
                            Console.Write("-Password: ");
                            pass = GetConsolePassword();

                        }
                        result = ubl.login(name,pass);
                        if( result != null   )
                        {
                            if(result.Type == 0)
                            {
                                id = result.User_id;
                                MenuCustomer();
                                break;

                            }else if( result.Type ==1)
                            {
                                MenuStaff.Menustaff();
                                break;
                            }
                        }else{
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(".  .  .Username or password wrong, please re-enter ! ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("You want to keep signing in?(y/n) :");
                            string chon = Console.ReadLine();
                            if(chon == "n")
                            {
                                break;
                            }
                            Console.Clear();
                        }

                    }
                    break;
                    case "2" :
                    Environment.Exit(2);
                    break;
                    default:
                    break;
                }
            }
        }
        public static  void MenuCustomer()
        {
            MENU menu = new MENU();
            
            while(true)
            {
                Console.Clear();
                short mainChoose = 0;
                string[] mainMenu = { "Display list shoes", "Shopping cart ", "Display list orders", "Log Out" };
                while( mainChoose!= mainMenu.Length)
                {
                    Console.Clear();
                    mainChoose = Menu("Men Shoes Store ", mainMenu);
                    switch(mainChoose)
                    {
                        case 1 :menu.Displaylistshoes();
                        break;
                        case 2 : menu.ShoppingCart();
                        break;
                        case 3 : menu.DisplayListOrders();
                        break;
                        case 4 :
                        menu.MainMENU();
                        break;
                        default :
                        break;
                    }

                }
            }
            

        } 
        public static string GetConsolePassword( )
        {
            StringBuilder sb = new StringBuilder( );
            while ( true )
            {
                ConsoleKeyInfo cki = Console.ReadKey( true );
                if ( cki.Key == ConsoleKey.Enter )
                {
                    Console.WriteLine( );
                    break;
                }

                if ( cki.Key == ConsoleKey.Backspace )
                {
                    if ( sb.Length > 0 )
                    {
                        Console.Write( "\b\0\b" );
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write( '*' );
                sb.Append( cki.KeyChar );
            }
            return sb.ToString( );
        }
        public  void Displaylistshoes()
        {
            Console.Clear();
            ShoesBL sbl = new ShoesBL();
            var lists = sbl.GetAllShoes();
            string line =("===================================================================================================================\n");
            Console.WriteLine(line);
            Console.Write(" Shoes_Id  |  Shoes_Name           |    Price         |  Size  |      Style             |   Color        | Amount |\n");
            Console.WriteLine(line);
            foreach (var Shoes in lists)
            {
                Console.WriteLine("{0,-15}{1,-25}{2,-17}{3,-10}{4,-24}{5,-18}{6} ",Shoes.Shoes_id,Shoes.Shoes_name,Shoes.Price,Shoes.Size,Shoes.Style,Shoes.Color,Shoes.Amount);
            }
            Console.WriteLine(line);
            Console.Write("Do you want to see product details?(y/n)");
            string choice = Console.ReadLine();
            if(choice == "y" )
            {
                Console.Write("Input  Shoes_ID: ");
                        while (true)
                        {
                            try
                            {
                                    sh_id = Convert.ToInt32(Console.ReadLine());
                                    var shoes = sbl.GetShoesById(sh_id);
                                    if(shoes == null)
                                    {
                                        throw new valueexception("Not find Shoes_ID,please re-enter : ");
                                    }
                                    else if(shoes != null)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("          Product Information        ");
                                        Console.WriteLine("-ID                : {0}",shoes.Shoes_id);
                                        Console.WriteLine("-Name              : {0}",shoes.Shoes_name);
                                        Console.WriteLine("-Size              : {0}",shoes.Size);
                                        Console.WriteLine("-Amount            : {0}",shoes.Amount);
                                        Console.WriteLine("-Price             : {0}dong",shoes.Price);
                                        Console.WriteLine("-Color             : {0}",shoes.Color);
                                        Console.WriteLine("-Material          : {0}",shoes.Material);
                                        Console.WriteLine("-Manufacture       : {0}",shoes.Manufacture);
                                        Console.WriteLine("-Trademark name    : {0}",shoes.TM.Name);
                                        Console.WriteLine("-Trademark Origin  : {0}",shoes.TM.Origin);
                                        while(true)
                                        {
                                           
                                            short mainChoose = 0;
                                            string[] mainMenu = { "Add in shopping cart", "Order now ", "Return shoes list" };
                                            while( mainChoose!= mainMenu.Length)
                                            {
                                                
                                                mainChoose = Menu2("You want to add to your shopping cart, buy or return to the shoes list? ", mainMenu);
                                            switch(mainChoose)
                                            {
                                                case 1  :
                                                Addshoppingcart();
                                                break;
                                                case  2 :
                                                OrderBL obl = new OrderBL();
                                                Orders orders = new Orders();
                                                orders.shoesList = new List<Shoes>();
                                                orders.Order_status = 1;
                                                orders.user = new User();
                                                orders.user.User_id =  id;
                                                orders.shoesList.Add(sbl.GetShoesById(sh_id)) ;
                                                var getshoes = sbl.GetShoesById(sh_id);
                                                while (true)
                                                {
                                                    try
                                                    {
                                                            Console.Write("Input  Amount: ");
                                                            int amount1 = Convert.ToInt32(Console.ReadLine());
                                                            if((amount1 > getshoes.Amount && getshoes.Amount == 0) || (amount1 == getshoes.Amount && getshoes.Amount == 0) )
                                                            {
                                                                Console.WriteLine("Quantity no longer please put another product .. !");
                                                                Console.ReadLine();
                                                                Displaylistshoes();
                                                                
                                                                }
                                                            if( amount1 > getshoes.Amount && getshoes.Amount >0 )
                                                            {
                                                                Console.WriteLine("Quantity in stock : {0}",getshoes.Amount);
                                                                throw (new valueexception("Not enough quantity , please re-enter: "));
                                                            }
                                                            else if( 0 < amount1 || amount1 <= getshoes.Amount)
                                                            {
                                                                orders.shoesList[0].Amount = amount1;
                                                                break;
                                                            }
                                                    }
                                                    catch(valueexception e)
                                                    {
                                                        Console.Write(e.Message);
                                                        continue;
                                                    }
                                                    catch
                                                    {
                                                        Console.Write("Please enter a valid  :");
                                                        continue;
                                                    }
                                                    break;
                                                }
                                                Console.WriteLine("Create Order: " + (obl.CreateOrder(orders) ? "completed!" : "not complete!"));
                                                Console.ReadLine();
                                                Displaylistshoes();
                                                break ;
                                                case 3 :
                                                Displaylistshoes();
                                                break;
                                                 default :
                                                break;
                                            }
                                            
                                            
                                        }
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
                                Console.WriteLine("FUCK1");
                                Console.Write("Please enter a valid value :");
                                continue;
                            }
                            break;
                     }
            }
            else if( choice == "n")
            {
                MenuCustomer();
            }
            
        }
        public bool Addshoppingcart()
        {
            OrderBL obl1 = new OrderBL();
            var listorders = obl1.GetAllOrderByIDUser(id);
            Orders order = new Orders();
            order.user = new User();
            order.shoesList = new List<Shoes>();
            int b = listorders.FindIndex(x => x.Order_status == 2);

            if(b != -1)
            {
                var shoes = sbl.GetShoesById(sh_id);
                int amount = 0;
                while(true)
                {
                    Console.Write("Input amount : ");
                    try{
                        amount = Convert.ToInt32(Console.ReadLine());
                        if( (amount > shoes.Amount && shoes.Amount == 0)  ||  ( amount == shoes.Amount && shoes.Amount == 0) )
                        {
                            Console.WriteLine("Quantity no longer please put another product .. !");
                            Console.ReadLine();
                            break;
                        }
                        else if( amount > shoes.Amount)
                        {
                            throw new valueexception("so luong khong du vui long nhap lai");
                        }
                        else if(0< amount && amount<shoes.Amount)
                        {
                             break;
                            
                        }
                    }
                    catch(valueexception e)
                    {
                        Console.Write(e.Message);
                        continue;

                    }catch{
                        Console.Write("Please enter a valid  :");
                        continue;
                    }
                }
                int dem  = 0;
                foreach (var or in listorders)
                {
                    if( or.Order_status == 2)
                    {
                        var detail = obl1.GetOrderDetailsByID(or.Order_id);
                        foreach(var shoess in detail.shoesList)
                        {
                            if( shoess.Shoes_id == sh_id)
                            {
                                obl1.updateAmount(amount,shoess.Shoes_id);
                                dem = 1;
                            }else if( shoes.Shoes_id != sh_id)
                            {
                                dem = 0;
                            }
                        }
                    }  
                }
                if( dem == 0)
                {
                    Console.WriteLine("Add in shopping cart: " + (obl1.InsertShoppingCarrt(listorders[b].Order_id,sh_id,amount) ? "completed!" : "not complete!"));                          
                    Console.ReadLine();
                    Displaylistshoes();

                }
                Console.WriteLine("Add in shopping cart completed ");                                    
                Console.ReadLine();
                Displaylistshoes();


            }else if(b == -1)
            {
                order.user.User_id = id;
                order.Order_status = 2;
                order.shoesList.Add(sbl.GetShoesById(sh_id)) ;
                var shoes = sbl.GetShoesById(sh_id);
                int amount;
                while(true)
                {
                    Console.Write("Input amount : ");
                    try{
                        amount = Convert.ToInt32(Console.ReadLine());
                        if( (amount > shoes.Amount && shoes.Amount == 0)  ||  ( amount == shoes.Amount && shoes.Amount == 0) )
                        {
                            Console.WriteLine("Quantity no longer please put another product .. !");
                            Console.ReadLine();
                            break;
                        }
                        else if( amount > shoes.Amount)
                        {
                            throw new valueexception("so luong khong du vui long nhap lai");
                        }
                        else if(0< amount && amount<shoes.Amount)
                        {
                            break;
                        }
                    }
                    catch(valueexception e)
                    {
                        Console.Write(e.Message);
                        continue;

                    }catch{
                        Console.Write("Please enter a valid  :");
                        continue;
                    }
                }
                order.shoesList[index].Amount = amount;
                index ++;
                
                Console.WriteLine("Add in shopping cart: " + (obl1.AddShppingCart(order) ? "completed!" : "not complete!"));            
                Console.ReadLine();
                Displaylistshoes();
            }
            return true;
        }
        public void ShoppingCart()
        {
            var Or_us = obl.GetAllOrderByIDUser(id);
            decimal a = 0;
            int b = Or_us.FindIndex(x => x.Order_status == 2);
            if(b == -1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The list is empty, no invoices have been generated!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadLine();
            }else if( b != -1)
            {
                string line = "============================================================================================";
                Console.WriteLine(line);
                foreach (var orders in Or_us)
                {
                    if( orders.Order_status == 2){
                       var orderdetail = obl.GetOrderDetailsByID(orders.Order_id);
                        Console.Clear();
                        Console.WriteLine("SHOES STORE");
                        Console.WriteLine("          Shoe Sales Invoice        ");
                        Console.WriteLine("-Order date        : {0}",orderdetail.Date_Order);
                        Console.WriteLine("-Order ID          : {0}",orderdetail.Order_id);
                        Console.WriteLine(line);
                        Console.Write(" Product name      |  Unitprice          |  Amount          |  Size     |     Total          \n");
                        foreach (var order in orderdetail.shoesList)
                        {
                            Decimal c = order.Price*order.Amount;
                            Console.WriteLine("{0,-20} {1,-26} {2,-15} {3,-10} {4}",order.Shoes_name,order.Price,order.Amount,order.Size,c);
                            a +=c;      
                        }
                        Console.WriteLine(line);
                        Console.WriteLine("-The total amount payable                                                  {0}",a);
                    }
                    
                }
                Console.WriteLine(line);
                Console.Write("Do you want to  order ?(y/n)");
                string choiceorder = Console.ReadLine();
                if(choiceorder == "y")
                {
                    Orders orders = new Orders();
                    orders.shoesList = new List<Shoes>();
                    orders.Order_status = 1;
                    orders.user = new User();
                    orders.user.User_id =  id;
                    foreach (var order in Or_us)
                    {
                        if(order.user.User_id == id)
                        {
                            if(order.Order_status == 2)
                            {
                                oredr_iddelete = order.Order_id;
                                var detail = obl.GetOrderDetailsByID(order.Order_id);
                                foreach( var shoes in detail.shoesList)
                                {
                                    var shoesbyid = sbl.GetShoesById(shoes.Shoes_id);
                                    if(shoes.Amount > shoesbyid.Amount )
                                    {
                                        Console.WriteLine(" Insufficient quantity Would you like to cancel this product? (y/n)",shoesbyid.Amount);
                                        char c = Convert.ToChar(Console.ReadLine());
                                        if(c == 'y')
                                        {
                                            obl.Delete(order.Order_id, shoes.Amount);
                                            break;
                                        }else if(c == 'n')
                                        {
                                        orders.shoesList.Add(sbl.GetShoesById(shoes.Shoes_id));            
                                        while(true)
                                        {
                                            Console.WriteLine("Please re-enter the product number: ");
                                            try{
                                                int  am = Convert.ToInt32(Console.ReadLine());
                                                if( (am > shoesbyid.Amount && shoesbyid.Amount == 0)  ||  ( am == shoesbyid.Amount && shoesbyid.Amount == 0) )
                                                {
                                                    Console.WriteLine("Quantity no longer please put another product .. !");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                                else if( am > shoesbyid.Amount)
                                                {
                                                    throw new valueexception("Quantity  not enough please re-enter");
                                                }
                                                else if(0< am && am<shoesbyid.Amount)
                                                {
                                                    orders.shoesList[index].Amount = am;
                                                    index++;
                                                    break;
                                                    }
                                                }catch(valueexception e)
                                                {
                                                Console.Write(e.Message);
                                                continue;
                                                }catch{
                                                    Console.WriteLine("Dung dep trai");
                                                    Console.Write("Please enter a valid  :");
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else if(0 < shoes.Amount && shoes.Amount < shoesbyid.Amount)
                                    {
                                        orders.shoesList.Add(sbl.GetShoesById(shoes.Shoes_id));
                                        orders.shoesList[index].Amount = shoes.Amount;
                                        index++; 
                                    }
                                }
                            }
                        }
                    }
                    
                    Console.WriteLine("Create Order: " + (obl.CreateOrder(orders) ? "completed!" : "not complete!"));
                    obl.DeleteShoppingCart(2,oredr_iddelete);
                    Console.ReadLine();
                    MenuCustomer();
                }
                else if( choiceorder == "n")
                {
                    MenuCustomer();
                }

            }
        }                
        public  void  DisplayListOrders()
        {
            Console.Clear();
            decimal a = 0;
            string line1 = "-------------------------------------------------------------------------\n";
            OrderBL obl = new OrderBL();
            var list = obl.GetAllOrderByIDUser(id);
            if(list.Count !=0)
            {
                string line = "========================================================================";
                Console.WriteLine(line);
                Console.WriteLine(" Order_id        |  Date                            |   Order Status  |");
                Console.WriteLine(line);
                foreach (var orders in list)
                {
                    Console.WriteLine("{0,-24}  {1,-33} {2}",orders.Order_id,orders.Date_Order,orders.Order_status);
                    
                }
                Console.WriteLine(line);
                Console.Write("Do you want to see Order details?(y/n)");
                string choice = Console.ReadLine();
                if(choice == "y" )
                {
                    Console.Write("Input  Order_ID: ");
                    while (true)
                    {
                        try
                        {
                            int or_id = Convert.ToInt32(Console.ReadLine());
                            var orderdetail = obl.GetOrderDetailsByID(or_id);
                            if(orderdetail == null)
                            {
                                throw new valueexception("Not find Order_ID,please re-enter : ");
                            }
                            else if(orderdetail != null)
                            {
                                Console.Clear();
                                Console.WriteLine("SHOES STORE");
                                Console.WriteLine("          Shoe Sales Invoice        ");
                                Console.WriteLine("-Order date        : {0}",orderdetail.Date_Order);
                                Console.WriteLine("-Order ID          : {0}",orderdetail.Order_id);
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
                                if(orderdetail.Order_status == 1)
                                {
                                    Console.WriteLine("The order is pending . . ");
                                }
                                else if( orderdetail.Order_status == 0)
                                {
                                    Console.WriteLine("The order has been processed .. ");
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
                            Console.WriteLine("FUCK2");
                            Console.Write("Please enter a valid value :");
                            continue;
                        }
                        break;
                    }
                }
                Console.Write("\n    Press Enter key to back menu... !");
                Console.ReadLine();
                
            }
            else if(list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The list is empty, no invoices have been generated!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadLine();
            }
        }
    }
}

