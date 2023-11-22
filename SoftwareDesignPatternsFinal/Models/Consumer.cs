using SoftwareDesignPatternsFinal.Enums;

namespace SoftwareDesignPatternsFinal.Models;

public class Consumer : User
{
    public string Address { get; set; }
    public Consumer(string firstName, string lastName, string password, string address, string phone) : base(firstName, lastName, password, phone, UserType.Consumer)
    {
        Address = address;
    }

    public Consumer(string firstName, string lastName, string password) : base(firstName, lastName, password)
    {
    }
}