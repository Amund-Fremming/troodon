using troodon.Cli;
using Spectre.Console;

namespace troodon.Core;

public class Orchestrator
{
    private string? ProjectName;
    private int NumberOfEntities;
    private IList<string>? Entities;
    private FolderStructure FolderStructure;

    private Crawler Crawler;

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
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    public async Task Build()
    {
        Crawler.MoveOut();
        Executor.BuildDotnetBase(ProjectName!);
        Crawler.MoveIn("troodon");

        if (FolderStructure == FolderStructure.Feature)
            await BuildFeature();

        if (FolderStructure == FolderStructure.Mvc)
            await BuildMvc();
    }

    private async Task BuildFeature()
    {
        try
        {
            double partOfHundred = Math.Floor(100.0 / Entities!.Count());
            IList<Task> tasks = new List<Task>();

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Processing...");
                    foreach (var entity in Entities!)
                    {
                        task.Increment(partOfHundred);

                        // Task<bool> entityTask = FeatureArchitecture(entity);
                        // tasks.Add(entityTask);
                    }

                    double restOfDivide = 100 % Entities!.Count();
                    task.Increment(restOfDivide);
                });

            await Task.WhenAll(tasks);
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private async Task BuildMvc()
    {
        try
        {
            // Build project and dependecies

            double partOfHundred = Math.Floor(100.0 / Entities!.Count());
            IList<Task> tasks = new List<Task>();

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Processing...");
                    foreach (var entity in Entities!)
                    {
                        task.Increment(partOfHundred);
                        // Opprett task
                        // Putt i Task array
                        // Task<bool> entityTask = MvcArchitecture(entity);
                        // tasks.Add(entityTask);
                    }

                    double restOfDivide = 100 % Entities!.Count();
                    task.Increment(restOfDivide);
                });

            // Kjør alle tasks async
            await Task.WhenAll(tasks);
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private async Task FeatureArchitecture(string entity)
    {
        try
        {
            // Build all files for this entity
            Crawler.CreateDir(entity);

            string entitySkeleton = Generator.Model(entity, ProjectName!);
            Crawler.CreateFile(entity);
            Crawler.WriteToFile(entity, entitySkeleton);

            string controller = $"{entity}Controller";
            string controllerSkeleton = Generator.Controller(entity, ProjectName!);
            Crawler.CreateFile(controller);
            Crawler.WriteToFile(controller, controllerSkeleton);

            string interf = $"I{entity}";
            string interfaceSkeleton = Generator.Interface(entity, ProjectName!);
            Crawler.CreateFile(interf);
            Crawler.WriteToFile(interf, interfaceSkeleton!);

            string service = $"{entity}Service";
            string serviceSkeleton = Generator.Service(entity, ProjectName!);
            Crawler.CreateFile(service);
            Crawler.WriteToFile(service, serviceSkeleton);

            string repository = $"{entity}Repository";
            string repositorySkeleton = Generator.Repository(entity, ProjectName!);
            Crawler.CreateFile(repository);
            Crawler.WriteToFile(service, serviceSkeleton);
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private async Task MvcArchitecture(string entity)
    {
        try
        {
            // Build files for one entity in respective folders
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private void BuildFeatureFolder()
    {
        try
        {
            // Create
            // Move in
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private void BuildMvcFolders()
    {
        try
        {
            // Create
            // Move in
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }

    private void BuildProjectFilesAndDependencies()
    {
        try
        {
            // Create dotnet project
            // Install folders
        }
        catch (Exception)
        {
            AnsiConsole.Markup("[bold red]Error[/][red]. Something went wrong, Try again. [/]");
        }
    }
}
