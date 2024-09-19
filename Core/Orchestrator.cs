using troodon.Cli;
using Spectre.Console;

namespace troodon.Core;

public class Orchestrator
{
    private IList<string> entities;

    public Orchestrator()
    {
        entities = new List<string>();
    }

    public void Build(Option option)
    {
        try
        {
            int numberOfEntries = Parser.GetNumberOfEntities();
            entities = Parser.GetEntityNames(numberOfEntries);

            double partOfHundred = 100 / entities.Count();

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Processing...");
                    foreach (var entity in entities)
                    {
                        // Generate the folders and so on here
                        task.Increment(partOfHundred);
                        Thread.Sleep(500);
                        task = ctx.AddTask("Generating");
                    }
                });
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }

    }

    private void FeatureArchitecture()
    {
        try
        {
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private void MvcArchitecture()
    {
        try
        {
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }
}
