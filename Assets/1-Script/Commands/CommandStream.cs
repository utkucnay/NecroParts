using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public static class CommandStream
{
    static Queue<ICommand> commands;

    public static void Init()
    {
        commands = new Queue<ICommand>();
    }

    public static void ExecuteCommands()
    {
        while (commands.Count > 0)
        {
            var command = commands.Dequeue();
            command.Execute();
        }
    }

    public static void AddCommand(ICommand command)
    {
        commands.Enqueue(command);
    }
}
