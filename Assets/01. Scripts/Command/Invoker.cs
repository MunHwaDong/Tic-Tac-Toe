using System.Collections;
using System.Collections.Generic;

public class Invoker
{
    public void AddCommand(ICommand command)
    {
        _command.Add(command);
    }
    
    public bool ExcuteCommands()
    {
        foreach (var command in _command)
        {
            if (command.Execute() is false)
                return false;
        }

        return true;
    }
    
    private readonly List<ICommand> _command = new();
}
