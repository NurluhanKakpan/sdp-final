using System.Security.AccessControl;

namespace SoftwareDesignPatternsFinal.Models;

public class Order
{
    public Order(
        int id,
        string fullName,
        string products,
        string supplier,
        string address,
        string phone)
    {
        Id = id;
        FullName = fullName;
        Products = products;
        Supplier = supplier;
        Address = address;
        Phone = phone;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Products { get; set; }
    public string Supplier { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}