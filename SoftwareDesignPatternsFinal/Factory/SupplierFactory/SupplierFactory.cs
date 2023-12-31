using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Enums;
using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Factory.SupplierFactory;

public class SupplierFactory : ISupplierFactory
{
    public Supplier? Create(string firstName, string lastName, string password)
    {
        var existSupplier = HaveUserAlreadyInDb(firstName, lastName);
        if (existSupplier)
            throw new Exception("Supplier already exist in db");
        InsertSupplier(firstName,lastName,password);
        return new Supplier(firstName, lastName, UserType.Supplier);
    }

    /// <summary>
    /// Search user from db 
    /// </summary>
    /// <param name="firstName">FirstName</param>
    /// <param name="lastName">LastName</param>
    /// <returns>True if user found/ otherwise False</returns>
    private static bool HaveUserAlreadyInDb(string firstName, string lastName)
    {
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            try
            {
                const string sql =
                    "SELECT COUNT(*) from Users WHERE FirstName = @FirstName AND LastName = @LastName AND UserType = @UserType";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@LastName", 1);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return count > 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    /// <summary>
    /// Add user to db
    /// </summary>
    /// <param name="firstName">FirstName</param>
    /// <param name="lastName">LastName</param>
    /// <param name="password">Password</param>
    /// <exception cref="Exception"></exception>
    private static void InsertSupplier(string firstName, string lastName, string password)
    {
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            try
            {
                const string sql =
                    "INSERT INTO Users (FirstName,LastName,Password,Address,Phone,UserType) VALUES(@FirstName,@LastName,@Password,@Address,@Phone,@UserType)";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Address", string.Empty);
                    command.Parameters.AddWithValue("@Phone", string.Empty);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@UserType", 1);
                    var rowEffected = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowEffected == 0)
                        throw new Exception("Supplier insertion failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}