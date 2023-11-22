using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Command;

public class CancelCommand : ICommandRepository
{
    public void ExecuteCommand(User user)
    {
        using (var connection = ApplicationDbContext.GetConnection.NpgsqlConnectionFactory)
        {
            connection.Open();
            const string sql = "DELETE FROM Orders Where FullName = @FullName";
            using (var command = new NpgsqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@FullName", user.FullName);
                var isSuccess = command.ExecuteNonQuery();
                if (isSuccess > 1)
                {
                    Console.WriteLine("Your orders all removed");
                }
            }
        }
    }
}