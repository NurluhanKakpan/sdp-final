using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Factory.ConsumerFactory;

public interface IConsumerFactory
{
    Consumer? Create(string firstName, string lastName, string password);
}