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
        try
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
            var newFilePath = Path.Combine(CurrentDir, name);
            File.Create(newFilePath).Dispose();
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
            var deleteFilePath = Path.Combine(CurrentDir, name);
            File.Delete(deleteFilePath);
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
        }
        catch (Exception e)
        {
            throw new Exception("WriteToFile: " + e.Message);
        }
    }
}
