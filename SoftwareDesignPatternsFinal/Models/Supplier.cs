using Npgsql;
using SoftwareDesignPatternsFinal.Database;
using SoftwareDesignPatternsFinal.Enums;
using SoftwareDesignPatternsFinal.Observer;

namespace SoftwareDesignPatternsFinal.Models;

public class Supplier : User
{
    public Supplier(string firstName, string lastName, string password, string phone) : base(firstName, lastName, password, phone, UserType.Supplier)
    {
    }

    public Supplier(string firstName, string lastName, string password) : base(firstName, lastName, password)
    {
    }

    public Supplier(string firstName, string lastName) : base(firstName, lastName, Enums.UserType.Supplier)
    {
        
    }
    public UserType GetUserType()
    {
        return UserType;
    }

    
}