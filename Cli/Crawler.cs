using Spectre.Console;

namespace troodon.Cli;

public class Crawler
{
    public string CurrentDir { get; private set; }

    public Crawler()
    {
        CurrentDir = Directory.GetCurrentDirectory();
        AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{CurrentDir}");
    }

    public void MoveIn(string dir)
    {
        try
        {
            CurrentDir = Path.Combine(CurrentDir, dir);
            AnsiConsole.Markup($"[yellow]Current dir: [/]{CurrentDir}");
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{CurrentDir}");
        }
    }

    public void MoveOut()
    {
        try
        {
            string parentDirectory = Directory.GetParent(CurrentDir)?.FullName!;
            CurrentDir = parentDirectory;
            AnsiConsole.MarkupLine($"[yellow]Current dir: [/]{CurrentDir}");
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
            AnsiConsole.MarkupLine($"[yellow]Created dir at {CurrentDir} : {name}[/]");
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
            AnsiConsole.MarkupLine($"[yellow]Created file at {CurrentDir} : {name}[/]");
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
            IList<string> files = Directory.GetFiles(CurrentDir);
            files.FirstOrDefault(_ => _ == file);

            File.WriteAllText(Path.Combine(CurrentDir, file), content);
            AnsiConsole.MarkupLine($"[yellow]Wrote to file at {CurrentDir} : {file}[/]");
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }
}
