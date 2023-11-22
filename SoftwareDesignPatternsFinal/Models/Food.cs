namespace SoftwareDesignPatternsFinal.Models;

public class Food
{
    public Food(string name,decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; set; }
    public decimal Price { get; set; } 
}