using Spectre.Console;
using System.Diagnostics;

namespace troodon.Cli;

public static class Executor
{
    public static void BuildDotnetBase(string projectName)
    {
        try
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "dotnet";
                process.StartInfo.Arguments = $"new webapi --name {projectName}";

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                process.WaitForExit();
            }
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
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "dotnet";
                // Add command for downloading EF Core
                process.StartInfo.Arguments = "";
                // dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
                // dotnet add package Microsoft.EntityFrameworkCore.Design
                // dotnet add package Swashbuckle.AspNetCore

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                process.WaitForExit();
            }
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }
}
