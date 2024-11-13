using Spectre.Console;

namespace troodon.Cli;

public class Crawler
{
    public string CurrentDir { get; private set; }

    public Crawler()
    {
        CurrentDir = Directory.GetCurrentDirectory();
    }

    public void MoveIn(string dir)
    {
        var newDir = Path.Combine(CurrentDir, dir);
        if (Directory.Exists(newDir))
        {
            CurrentDir = newDir;
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Directory '{newDir}' does not exist.[/]");
        }
    }

    public void MoveOut()
    {
        string? parentDirectory = Directory.GetParent(CurrentDir)?.FullName;
        if (parentDirectory != null)
        {
            CurrentDir = parentDirectory;
            return;
        }

        AnsiConsole.MarkupLine($"[red]Already at the root directory, can't move out further.[/]");
    }

    public void CreateDir(string name)
    {
        var newDir = Path.Combine(CurrentDir, name);
        Directory.CreateDirectory(newDir);
    }

    public void CreateFile(string name)
    {
        var newFilePath = Path.Combine(CurrentDir, name);
        File.Create(newFilePath).Dispose();
    }

    public void DeleteFile(string name)
    {
        var deleteFilePath = Path.Combine(CurrentDir, name);
        File.Delete(deleteFilePath);
    }

    public void WriteToFile(string file, string content)
    {
        file = file + ".cs";
        var filePath = Path.Combine(CurrentDir, file);
        File.WriteAllText(filePath, content);
    }
}