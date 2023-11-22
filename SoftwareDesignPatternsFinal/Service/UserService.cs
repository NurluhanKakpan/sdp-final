using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Enums;
using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Service;

public class UserService
{
    public static User? GetUser(string firstName, string lastName, string password)
    {
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            const string sql =
                "SELECT * FROM Users WHERE FirstName = @FirstName AND LastName = @LastName AND Password = @Password";
            using (var command  = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Password", password);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userFirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        var userLastName = reader.GetString(reader.GetOrdinal("LastName"));
                        var address = reader.GetString(reader.GetOrdinal("Address"));
                        var phone = reader.GetString(reader.GetOrdinal("Phone"));
                        var userType = (UserType)reader.GetInt32(reader.GetOrdinal("UserType"));
                        if (userType == UserType.Consumer)
                            return new Consumer(userFirstName, userLastName, password, address, phone);
                        return new Supplier(userFirstName, userLastName, password, phone);    
                    }
                }
            }
        }
        return null;
    }
}