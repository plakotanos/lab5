using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Lab3.Commands;
using Lab3.Data;
using Lab3.Models;
using Lab3.Models.Commands;
using ReactiveUI;

namespace Lab5.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly CommandExecutor _executor = new();
    
    private string _command = string.Empty;
    public string Command
    {
        get => _command;
        set => this.RaiseAndSetIfChanged(ref _command, value);
    }

    public TurtleViewModel Turtle { get; }

    public ObservableCollection<string> History { get; } = new();
    
    public ReactiveCommand<Unit, Unit> ExecuteCommand { get; }

    public MainWindowViewModel()
    {
        var context = new TurtleContext();
        Turtle = new (context.Turtles.FirstOrDefault() ?? new Turtle());

        _executor.AddCommands(
            new AngleCommand(Turtle.Value),
            new ColorCommand(Turtle.Value),
            new MoveCommand(Turtle.Value),
            new PenDownCommand(Turtle.Value),
            new PenUpCommand(Turtle.Value)
        );

        var executeEnabled = this.WhenAnyValue(
            x => x.Command,
            _executor.CommandIsValid
        );

        ExecuteCommand = ReactiveCommand.Create(
            Execute,
            executeEnabled
        );
    }
    
    private void Execute()
    {
        _executor.TryExecuteCommand(Command);

        History.Add(Command);
        
        var context = new TurtleContext();
        Turtle.Value = (
            from t in context.Turtles
            where t.Id == Turtle.Value.Id
            select t
        ).FirstOrDefault()!;
    }
}