using troodon.Cli;
using Spectre.Console;

namespace troodon.Core;

public class Orchestrator
{
    private string? ProjectName;
    private int NumberOfEntities;
    private IList<string>? Entities;
    private FolderStructure FolderStructure;

    public Orchestrator()
    {
        GetProjectData();
    }

    private void GetProjectData()
    {
        try
        {
            ProjectName = Parser.GetProjectName();
            NumberOfEntities = Parser.GetNumberOfEntities();
            Entities = Parser.GetEntityNames(NumberOfEntities);
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public void Build()
    {
        try
        {
            double partOfHundred = Math.Floor(100.0 / Entities!.Count());

            Func<bool> BuildFunction = FolderStructure == FolderStructure.Mvc ? FeatureArchitecture : MvcArchitecture;

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Processing...");
                    foreach (var entity in Entities!)
                    {
                        task.Increment(partOfHundred);
                    }

                    double restOfDivide = 100 % Entities!.Count();
                    task.Increment(restOfDivide);
                });
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private bool FeatureArchitecture()
    {
        try
        {
            return false;
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
            return false;
        }
    }

    private bool MvcArchitecture()
    {
        try
        {
            return false;
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
            return false;
        }
    }
}
