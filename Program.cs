using Spectre.Console;

namespace troodon;

public class Program
{
    static void Main(String[] args)
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing...");
                for (var i = 0; i < 100; i++)
                {
                    task.Increment(1);
                    Thread.Sleep(50); // Simulate work
                }
            });


        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("Age");
        table.AddColumn("Occupation");

        table.AddRow("Alice", "30", "Engineer");
        table.AddRow("Bob", "25", "Designer");
        table.AddRow("Charlie", "35", "Manager");

        AnsiConsole.Write(table);


        AnsiConsole.Markup("[bold green]Success![/] [yellow]This is a message with bold and color.[/]");
        AnsiConsole.MarkupLine("[blue]This line is blue.[/]");


        var tree = new Tree("Root");
        tree.AddNode("Child 1");
        tree.AddNode("Child 2")
            .AddNode("Grandchild 1");
        tree.AddNode("Child 3");

        AnsiConsole.Write(tree);


        var name = AnsiConsole.Ask<string>("What is your [green]name[/]?");
        var age = AnsiConsole.Ask<int>("How old are you [blue]?[/])");

        AnsiConsole.MarkupLine($"Hello [bold yellow]{name}[/]! You are [bold cyan]{age}[/] years old.");


        var choice = AnsiConsole.Prompt(
                  new SelectionPrompt<string>()
                      .Title("Select an option:")
                      .AddChoices("Option 1", "Option 2", "Option 3")
              );

        AnsiConsole.MarkupLine($"You selected: [bold]{choice}[/]");
    }
}
