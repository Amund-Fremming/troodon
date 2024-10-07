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
        catch (Exception e)
        {
            throw new Exception("GetProjectData: " + e.Message);
        }
    }

    public void Build()
    {
        Executor.BuildDotnetBase(ProjectName!);

        if (FolderStructure == FolderStructure.Feature)
        {
            BuildFeatureFolder();
            BuildFeature();
        }

        if (FolderStructure == FolderStructure.Mvc)
        {
            BuildMvcFolders();
            // BuildMvc();
        }
    }

    private void BuildFeature()
    {
        try
        {
            foreach (var entity in Entities!)
            {
                FeatureArchitecture(entity);
            }
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeature: " + e.Message);
        }
    }

    private void BuildMvc()
    {
        try
        {
            // Build project and dependecies

            double partOfHundred = Math.Floor(100.0 / Entities!.Count());
            // IList<Task> tasks = new List<Task>();

            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Processing...");
                    foreach (var entity in Entities!)
                    {
                        task.Increment(partOfHundred);
                        // Opprett task
                        // Putt i Task array
                        // Task entityTask = MvcArchitecture(entity);
                        // tasks.Add(entityTask);
                    }

                    double restOfDivide = 100 % Entities!.Count();
                    task.Increment(restOfDivide);
                });

            // Kjør alle tasks async
            // await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            throw new Exception("BuildMvc: " + e.Message);
        }
    }

    private void FeatureArchitecture(string entity)
    {
        try
        {
            Crawler.CreateDir(entity);
            Crawler.MoveIn(entity);

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

            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("FeatureArchitecture: " + e.Message);
        }
    }

    private async Task MvcArchitecture(string entity)
    {
        try
        {
            // Build files for one entity in respective folders
        }
        catch (Exception e)
        {
            throw new Exception("MvcArchitecture: " + e.Message);
        }
    }

    private void BuildFeatureFolder()
    {
        try
        {
            Crawler.MoveIn(ProjectName!);
            Crawler.CreateDir("Features");
            Crawler.MoveIn("Features");
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeatureFolder: " + e.Message);
        }
    }

    private void BuildMvcFolders()
    {
        try
        {
            // Create
            // Move in
        }
        catch (Exception e)
        {
            throw new Exception("FeatureArchitecture: " + e.Message);
        }
    }

    private void BuildProjectFilesAndDependencies()
    {
        try
        {
            // Create dotnet project
            // Install folders
        }
        catch (Exception e)
        {
            throw new Exception("BuildProjectFilesAndDependencies: " + e.Message);
        }
    }
}
