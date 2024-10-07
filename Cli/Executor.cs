using Spectre.Console;
using System.Diagnostics;

namespace troodon.Cli;

public static class Executor
{
    private static void RunCommand(string command)
    {
        using (Process process = new Process())
        {
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = command;

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

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

    public static void FetchNuGets()
    {
        try
        {
            string commandOne = "dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL";
            string commandTwo = "dotnet add package Microsoft.EntityFrameworkCore.Design";
            string commandThree = "dotnet add package Swashbuckle.AspNetCore";

            RunCommand(commandOne);
            RunCommand(commandTwo);
            RunCommand(commandThree);

        }
        catch (Exception e)
        {
            throw new Exception("FetchNuGets: " + e.Message);
        }
    }
}
