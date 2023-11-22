using SoftwareDesignPatternsFinal.Factory.ConsumerFactory;
using SoftwareDesignPatternsFinal.Factory.SupplierFactory;
using SoftwareDesignPatternsFinal.Models;
using SoftwareDesignPatternsFinal.Service;

namespace SoftwareDesignPatternsFinal.Auth;

public class Auth
{
    private readonly ISupplierFactory _supplierFactory;
    private readonly IConsumerFactory _consumerFactory;

    public Auth(
        ISupplierFactory supplierFactory, 
        IConsumerFactory consumerFactory)
    {
        _supplierFactory = supplierFactory;
        _consumerFactory = consumerFactory;
    }

    public User? Register()
    {
        Console.WriteLine("Input your FirstName");
        var firstName = Console.ReadLine();
        Console.WriteLine("Input Your LastName");
        var lastName = Console.ReadLine();
        Console.WriteLine("Input Your Password");
        var password = Console.ReadLine();
        Console.WriteLine("Choose your user type");
        Console.WriteLine("""
                          1)Supplier
                          2)Consumer
                          """);
        var userType = Console.ReadLine();
        var user = new User();
        switch (userType)
        {
            case "1":
                user = _supplierFactory.Create(firstName, lastName, password);
                return user;
            case "2":
                user = _consumerFactory.Create(firstName, lastName, password);
                return user;
        }
        
        return user;
    }

    public User? Login()
    {
        Console.WriteLine("Input your FirstName");
        var firstName = Console.ReadLine();
        Console.WriteLine("Input Your LastName");
        var lastName = Console.ReadLine();
        Console.WriteLine("Input Your Password");
        var password = Console.ReadLine();
        var userService = new UserService();
        var user = UserService.GetUser(firstName, lastName, password);
        if (user == null)
            throw new Exception("User not found");
        return user;
    }
}