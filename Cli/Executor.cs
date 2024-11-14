using System;
using System.Diagnostics;
using System.IO;

namespace troodon.Cli;

public static class Executor
{
    private static void RunCommand(string command, string workingDirectory = "")
    {
        using Process process = new Process();

        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = command;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        if (!string.IsNullOrEmpty(workingDirectory))
        {
            process.StartInfo.WorkingDirectory = workingDirectory;
        }

        process.Start();
        process.WaitForExit();
    }

    public static void MoveProject(string projectName)
    {
        string projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), projectName);
        string parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        string targetPath = Path.Combine(parentDirectory, projectName);

        Directory.Move(projectDirectory, targetPath);
    }

    public static void BuildDotnetBase(string projectName)
    {
        try
        {
            string command = $"new webapi --name {projectName}";
            RunCommand(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void RunMigration(string projectName)
    {
        try
        {
            string commandOne = "ef migrations add Init";
            string commandTwo = "ef database update";
            RunCommand(commandOne);
            RunCommand(commandTwo);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void CreateSln(string projectName)
    {
        try
        {
            string parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
            string targetPath = Path.Combine(parentDirectory, projectName);

            string commandOne = "new sln";
            string commandTwo = $"sln add {projectName}.csproj";

            RunCommand(commandOne, targetPath);
            RunCommand(commandTwo, targetPath);
        }
        catch (Exception)
        {
            throw;
        }
    }
}