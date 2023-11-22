using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Factory.SupplierFactory;

public interface ISupplierFactory
{
    Supplier Create(string firstName, string lastName, string password);
}