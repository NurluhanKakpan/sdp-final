using SoftwareDesignPatternsFinal.Models;

namespace SoftwareDesignPatternsFinal.Command;

public interface ICommandRepository
{
    void ExecuteCommand(User user);
}