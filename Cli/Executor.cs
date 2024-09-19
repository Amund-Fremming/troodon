using Spectre.Console;

namespace troodon.Cli;

public static class Executor
{
    public static void BuildDotnetBase()
    {
        try
        {
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public static void FetchNuGets()
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
