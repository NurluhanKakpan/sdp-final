using Npgsql;
using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Database;

public class ApplicationDbContext
{
   private const string ConnectionString = "Host=localhost;Port=5432;Database=final;Username=postgres;Password=050724;";
   private static ApplicationDbContext _dbContext;
   private static NpgsqlConnection _connection;
   public  static readonly Food[] Products =
   {
      new("Milk",250),
      new("Bread",150),
      new("Apple",180),
      new("Banana",400),
      new("Meat",250)
   }; 

   public NpgsqlConnection NpgsqlConnectionFactory => _connection;

   private ApplicationDbContext()
   {
      
   }

   public static ApplicationDbContext GetConnection
   {
      get
      {
         if (_dbContext == null)
            _dbContext = new ApplicationDbContext();
         _connection = new NpgsqlConnection(ConnectionString);
         return _dbContext;
      }
   }
}