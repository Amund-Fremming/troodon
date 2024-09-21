using Spectre.Console;

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
            Directory.CreateDirectory(name);
            AnsiConsole.MarkupLine($"[yellow]Created dir at {_currentDir} : {name}[/]");
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
            File.Create(name);
            AnsiConsole.MarkupLine($"[yellow]Created file at {_currentDir} : {name}[/]");
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public void WriteToFile(string file, string content)
    {
        try
        {
            IList<string> files = Directory.GetFiles(_currentDir);
            files.FirstOrDefault(_ => _ == file);

            File.WriteAllText(Path.Combine(_currentDir, file), content);
            AnsiConsole.MarkupLine($"[yellow]Wrote to file at {_currentDir} : {file}[/]");
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }
}
