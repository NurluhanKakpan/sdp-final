using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Command;

public class OrderCommand : ICommandRepository
{
    private IDictionary<int, int> _products;

    public OrderCommand(IDictionary<int, int> products)
    {
        _products = products;
    }

    public void ExecuteCommand(User user)
    {
        Console.WriteLine("Write your address to send order");
        var address = Console.ReadLine();
        Console.WriteLine("Write your phone to connect with you");
        var phone = Console.ReadLine();
        var insertProducts = string.Empty;
        foreach (var key in _products.Keys)
        {
            var countOfProduct = _products[key];
            var product = ApplicationDbContext.Products[key].Name;
            insertProducts += $"{countOfProduct}*{product};";
        }
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();

            const string sql = "INSERT INTO Orders (FullName,Products,Supplier,Address,Phone) VALUES(@FullName,@Products,@Supplier,@Address,@Phone)";
            using (var command = new NpgsqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@Products", insertProducts);
                command.Parameters.AddWithValue("@Supplier", string.Empty);
                command.Parameters.AddWithValue("@Address", address!);
                command.Parameters.AddWithValue("@Phone", phone!);
                var rowEffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowEffected == 0)
                    throw new Exception("Supplier insertion failed");
            }
        }
        Console.WriteLine("Your order created successfully, shortly we will connect with you");
    }
}