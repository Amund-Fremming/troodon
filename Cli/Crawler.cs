using Spectre.Console;
using System.IO;

namespace troodon.Cli;

public class Crawler
{
    public string _currentDir { get; private set; }

    public Crawler()
    {
        _currentDir = Directory.GetCurrentDirectory();
        AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{_currentDir}");
    }

    public void MoveIn(string dir)
    {
        try
        {
            _currentDir = Path.Combine(_currentDir, dir);
            AnsiConsole.Markup($"[yellow]Current dir: [/]{_currentDir}");
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{_currentDir}");
        }
    }

    public void MoveOut()
    {
        try
        {
            string parentDirectory = Directory.GetParent(_currentDir)?.FullName!;
            _currentDir = parentDirectory;
            AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{_currentDir}");
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public void CreateDir(string name)
    {
        try
        {
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public void CreateFile(string name)
    {
        try
        {
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public void WriteToFile(string content)
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
