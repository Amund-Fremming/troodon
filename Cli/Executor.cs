using Spectre.Console;
using System.Diagnostics;

namespace troodon.Cli;

public static class Executor
{
    private static void RunCommand(string command, string workingDirectory = "")
    {
        using (Process process = new Process())
        {
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
    }

    public static void BuildDotnetBase(string projectName)
    {
        try
        {
            string command = $"new webapi --name {projectName}";
            RunCommand(command);
        }
        catch (Exception e)
        {
            throw new Exception("BuildDotnetBase: " + e.Message);
        }
    }

    public static void RunMigration(string projectName)
    {
        try
        {
            throw new NotImplementedException("Yeah, not implemented buddy...");
        }
        catch (Exception e)
        {
            throw new Exception("BuildDotnetBase: " + e.Message);
        }
    }

    public static void FetchEfNuGets(string projectName)
    {
        try
        {
            string projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), projectName);

            string commandOne = "add package Npgsql.EntityFrameworkCore.PostgreSQL";
            string commandTwo = "add package Microsoft.EntityFrameworkCore.Design";
            string commandThree = "add package Swashbuckle.AspNetCore";

            RunCommand(commandOne, projectDirectory);
            RunCommand(commandTwo, projectDirectory);
            RunCommand(commandThree, projectDirectory);
        }
        catch (Exception e)
        {
            throw new Exception("FetchNuGets: " + e.Message);
        }
    }

    public static void FetchInMemoryNuGets(string projectName)
    {
        try
        {
            string projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), projectName);

            string commandOne = "add package Microsoft.EntityFrameworkCore.InMemory";
            string commandThree = "add package Swashbuckle.AspNetCore";

            RunCommand(commandOne, projectDirectory);
            RunCommand(commandThree, projectDirectory);
        }
        catch (Exception e)
        {
            throw new Exception("FetchNuGets: " + e.Message);
        }
    }
}
