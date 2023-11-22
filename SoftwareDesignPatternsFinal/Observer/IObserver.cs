using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Observer;

public interface IObserver
{
    public void Update(Order order);
}