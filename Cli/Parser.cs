using Spectre.Console;

namespace troodon.Cli;

public static class Parser
{
    public static string GetProjectName()
    {
        try
        {
            return AnsiConsole.Ask<string>("Whats the project [blue bold]name[/]?");
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
            return "";
        }
    }

    public static int GetNumberOfEntities()
    {
        try
        {
            int numberOfEntries = AnsiConsole.Ask<int>("How many [blue bold]models[/]?");
            return numberOfEntries;
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
            return 0;
        }
    }

    public static IList<string> GetEntityNames(int numberOfModels)
    {
        try
        {
            IList<string> entityNames = new List<string>();

            for (int i = 0; i < numberOfModels; i++)
            {
                string entity = AnsiConsole.Ask<string>($"[red bold]({(i + 1)})[/] Name: ");
                entityNames.Add(entity);
            }

            return entityNames;
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
            return [];
        }
    }
}
