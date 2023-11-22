using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Enums;
using SoftwareDesignPatternsFinal.Observer;

namespace SoftwareDesignPatternsFinal.Models;

public class User : IObserver
{
    public User(string firstName, string lastName, string password, string phone,UserType userType)
    {
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Phone = phone;
        UserType = userType;
    }

    public User(string firstName, string lastName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public User()
    {
        
    }

    public User(string firstName, string lastName, UserType userType)
    {
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    
    public UserType UserType { get; set; }

    public UserType GetUserType()
    {
        return UserType;
    }

    public string FullName => $"{FirstName}_{LastName}";
    
    public void Update(Order order)
    {
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            const string sql = "update orders set supplier = @supplier where id = @id";
            using (var command = new NpgsqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@supplier", FullName);
                command.Parameters.AddWithValue("@id", order.Id);
                var saveChanges = command.ExecuteNonQuery();
                if (saveChanges  > 0)
                {
                    Console.WriteLine($"You are take this order. OwnerName is {order.FullName}, Address : {order.Address} Phone : {order.Phone}");
                }
            }
        }   
    }
}