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
            var newDir = Path.Combine(CurrentDir, dir);
            if (Directory.Exists(newDir))
            {
                CurrentDir = newDir;
                AnsiConsole.MarkupLine($"[yellow]Moved in. Current dir: [/]{CurrentDir}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Directory '{newDir}' does not exist.[/]");
            }
        }
        catch (Exception e)
        {
            throw new Exception("MoveIn: " + e.Message);
        }
    }

    public void MoveOut()
    {
        try
        {
            string? parentDirectory = Directory.GetParent(CurrentDir)?.FullName;
            if (parentDirectory != null)
            {
                CurrentDir = parentDirectory;
                AnsiConsole.MarkupLine($"[yellow]Moved out. Current dir: [/]{CurrentDir}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Already at the root directory, can't move out further.[/]");
            }
        }
        catch (Exception e)
        {
            throw new Exception("MoveOut: " + e.Message);
        }
    }

    public void CreateDir(string name)
    {
        try
        {
            var newDir = Path.Combine(CurrentDir, name);
            Directory.CreateDirectory(newDir);
            AnsiConsole.MarkupLine($"[yellow]Created dir at {newDir}[/]");
        }
        catch (Exception e)
        {
            throw new Exception("CreateDir: " + e.Message);
        }
    }

    public void CreateFile(string name)
    {
        try
        {
            name = name + ".cs";
            var newFilePath = Path.Combine(CurrentDir, name);
            File.Create(newFilePath).Dispose();
            AnsiConsole.MarkupLine($"[yellow]Created file at {newFilePath}[/]");
        }
        catch (Exception e)
        {
            throw new Exception("CreateFile: " + e.Message);
        }
    }

    public void DeleteFile(string name)
    {
        try
        {
            name = name + ".cs";
            var deleteFilePath = Path.Combine(CurrentDir, name);
            File.Delete(deleteFilePath);
            AnsiConsole.MarkupLine($"[yellow]Deleted file at {deleteFilePath}[/]");
        }
        catch (Exception e)
        {
            throw new Exception("DeleteFile: " + e.Message);
        }
    }

    public void WriteToFile(string file, string content)
    {
        try
        {
            file = file + ".cs";
            var filePath = Path.Combine(CurrentDir, file);
            File.WriteAllText(filePath, content);
            AnsiConsole.MarkupLine($"[yellow]Wrote to file at {filePath}[/]");
        }
        catch (Exception e)
        {
            throw new Exception("WriteToFile: " + e.Message);
        }
    }
}
