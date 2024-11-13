using Spectre.Console;
using troodon.Cli;

namespace troodon.Core;

public class Orchestrator
{
    private string? ProjectName { get; set; }
    private int NumberOfEntities { get; set; }
    private IList<string>? Entities { get; set; }

    private readonly Crawler Crawler;

    public Orchestrator()
    {
        GetProjectData();
        this.Crawler = new Crawler();
    }

    private void GetProjectData()
    {
        try
        {
            ProjectName = Parser.GetProjectName();
            NumberOfEntities = Parser.GetNumberOfEntities();
            Entities = Parser.GetEntityNames(NumberOfEntities);
        }
        catch (Exception e)
        {
            throw new Exception("GetProjectData: " + e.Message);
        }
    }

    public void Build()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arc)
            .SpinnerStyle(Style.Parse("blue"))
            .Start("Generating project", ctx =>
            {
                Executor.BuildDotnetBase(ProjectName!);

                ctx.Status("Generating architecture");
                Crawler.MoveIn(ProjectName!);
                Crawler.CreateDir("Features");

                BuildProgramCs();
                BuildFeatures();
                BuildDbContext();
            });

        Executor.MoveProject(ProjectName!);
        Executor.CreateSln(ProjectName!);

        var table = new Table();
        table.AddColumn("[blue]Sucess! Now add a connection string, install and run EF Core migrations[/]");
        table.AddRow("dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL");
        table.AddRow("dotnet add package Microsoft.EntityFrameworkCore.Design");
        table.AddRow("dotnet add package Swashbuckle.AspNetCore");
        table.AddRow("");
        table.AddRow("dotnet ef migrations add Init");
        table.AddRow("dotnet ef database update");
        AnsiConsole.Write(table);
    }

    private void BuildFeatures()
    {
        try
        {
            Crawler.MoveIn("Features");

            foreach (var entity in Entities!)
            {
                BuildFiles(entity);
            }

            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeatures: " + e.Message);
        }
    }

    private void BuildFiles(string entity)
    {
        try
        {
            Crawler.CreateDir(entity);
            Crawler.MoveIn(entity);

            string model = $"{entity}Model";
            string entitySkeleton = Generator.Model(entity, ProjectName!);
            Crawler.CreateFile(model + ".cs");
            Crawler.WriteToFile(model, entitySkeleton);

            string controller = $"{entity}Controller";
            string controllerSkeleton = Generator.Controller(entity, ProjectName!);
            Crawler.CreateFile(controller + ".cs");
            Crawler.WriteToFile(controller, controllerSkeleton);

            string serviceInterface = $"I{entity}Service";
            string iServiceSkeleton = Generator.Interface(entity, ProjectName!, "service");
            Crawler.CreateFile(serviceInterface + ".cs");
            Crawler.WriteToFile(serviceInterface, iServiceSkeleton!);

            string repositoryInterface = $"I{entity}Repository";
            string iRepoSkeleton = Generator.Interface(entity, ProjectName!, "repository");
            Crawler.CreateFile(repositoryInterface + ".cs");
            Crawler.WriteToFile(repositoryInterface, iRepoSkeleton!);

            string service = $"{entity}Service";
            string serviceSkeleton = Generator.Service(entity, ProjectName!);
            Crawler.CreateFile(service + ".cs");
            Crawler.WriteToFile(service, serviceSkeleton);

            string repository = $"{entity}Repository";
            string repositorySkeleton = Generator.Repository(entity, ProjectName!);
            Crawler.CreateFile(repository + ".cs");
            Crawler.WriteToFile(repository, repositorySkeleton);

            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("BuildFiles: " + e.Message);
        }
    }

    private void BuildDbContext()
    {
        try
        {
            var dir = "App";
            var file = "AppDbContext";

            Crawler.CreateDir(dir);
            Crawler.MoveIn(dir);
            Crawler.CreateFile(file + ".cs");

            var contextSkeleton = Generator.DbContext(Entities!, ProjectName!);
            Crawler.WriteToFile(file, contextSkeleton);

            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("CreateDbContext: " + e.Message);
        }
    }

    private void BuildProgramCs()
    {
        try
        {
            var file = "Program";

            Crawler.DeleteFile(file + ".cs");
            Crawler.CreateFile(file + ".cs");

            var programSkeleton = Generator.Program(Entities!, ProjectName!);
            Crawler.WriteToFile(file, programSkeleton);
        }
        catch (Exception e)
        {
            throw new Exception("CreateDbContext: " + e.Message);
        }
    }
}