using Npgsql;
using SoftwareDesignPatternsFinal.Auth;
using SoftwareDesignPatternsFinal.Command;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Enums;
using SoftwareDesignPatternsFinal.Factory.ConsumerFactory;
using SoftwareDesignPatternsFinal.Factory.SupplierFactory;
using SoftwareDesignPatternsFinal.Models;

class Program
{
    static void Main(string[] args)
    {
      Console.WriteLine("HELLO USER");
      Console.WriteLine("WELCOME TO FOOD DELIVERY APP");
      var user = Auth();
      var userType = user?.GetUserType();
      if (userType == UserType.Consumer)
      { 
        DoOrder(user);
        Console.WriteLine("Choose next action");
        Console.WriteLine("""
                          1)Cancel order
                          2)Exit
                          """);
        var action = Console.ReadLine();
        if(action == "2")
            Environment.Exit(0);
        else
        {
            CancelOrder(user);
        }
      }
      else
      {
          var order = GetOrder(user);
          if (order != null)
          {
              Console.WriteLine($"You are take a order. Address is  {order.Address}");
              user.Update(order);
          }
      }
    }

    private static User? Auth()
    {
        Console.WriteLine("""
                          1) Register 
                          2) Login
                          3) Turn off
                          """);

        var choose = Console.ReadLine();
        var auth = new Auth(new SupplierFactory(), new ConsumerFactory());
        switch (choose)
        {
            case "1":
                return auth.Register();
            case "2":
                return auth.Login();
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Exiting");
                Environment.Exit(0);
                break;
        }
        return new User();
    }

    private static void DoOrder(User? user)
    {
        var chooseProducts = ChooseProducts();
        ConfirmOrder(chooseProducts, user);
        
    }

    private static void CancelOrder( User user)
    {
        new CancelCommand().ExecuteCommand(user);
    }   
    
    private static Order? GetOrder(User? user)
    {
        var id = 0;
        var fullName = "";
        var products = "";
        var address = "";
        var phone = "";
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            const string sql = "SELECT * FROM orders WHERE supplier = '' LIMIT 1";
            
            using (var command = new NpgsqlCommand(sql,connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("Id"));
                        fullName = reader.GetString(reader.GetOrdinal("FullName"));
                        products = reader.GetString(reader.GetOrdinal("Products"));
                        address = reader.GetString(reader.GetOrdinal("Address"));
                        phone = reader.GetString(reader.GetOrdinal("Phone"));
                    }
                }
            }
        }

        return new Order(id,fullName, products, user.FullName, address, phone);
    }
    private static Dictionary<int, int> ChooseProducts()
    {
        var productsToDelivery = ApplicationDbContext.Products;
        for (var i = 0; i < productsToDelivery.Length; i++)
        {
            Console.WriteLine($"""
                               {i}) Name {productsToDelivery[i].Name}
                               Price {productsToDelivery[i].Price}
                               """);
        }
        var chooseProducts = Console.ReadLine()!.Split(" ").Select(int.Parse).GroupBy(q=>q).ToDictionary(q=>q.Key,q=>q.Count());
        return chooseProducts;
    }

    private static void ConfirmOrder(IDictionary<int, int> products, User? user)
    {
        Console.WriteLine("""

                          Are you sure to choose order
                          """);
        var choice = Console.ReadLine();
        if(choice!.ToLower() == "yes")
            new OrderCommand(products).ExecuteCommand(user);
    }
    
}
