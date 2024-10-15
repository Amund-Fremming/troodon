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
        Executor.FetchNuGets(ProjectName!);
        Crawler.MoveIn(ProjectName!);

        if (FolderStructure == FolderStructure.Feature)
        {
            Crawler.CreateDir("Features");
            BuildFeatures();
        }

        if (FolderStructure == FolderStructure.Mvc)
        {
            BuildMvcFolders();
            // BuildMvc();
        }

        BuildProgramCs();
        BuildDbContext();
    }

    private void BuildFeatures()
    {
        try
        {
            Crawler.MoveIn("Features");
            foreach (var entity in Entities!)
            {
                BuildFeatureFiles(entity);
            }
            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeatures: " + e.Message);
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

    private void BuildFeatureFiles(string entity)
    {
        try
        {
            Crawler.CreateDir(entity);
            Crawler.MoveIn(entity);

            string model = $"{entity}Model";
            string entitySkeleton = Generator.Model(entity, ProjectName!);
            Crawler.CreateFile(model);
            Crawler.WriteToFile(model, entitySkeleton);

            string controller = $"{entity}Controller";
            string controllerSkeleton = Generator.Controller(entity, ProjectName!);
            Crawler.CreateFile(controller);
            Crawler.WriteToFile(controller, controllerSkeleton);

            string serviceInterface = $"I{entity}Service";
            string iServiceSkeleton = Generator.Interface(entity, ProjectName!, "service");
            Crawler.CreateFile(serviceInterface);
            Crawler.WriteToFile(serviceInterface, iServiceSkeleton!);

            string repositoryInterface = $"I{entity}Repository";
            string iRepoSkeleton = Generator.Interface(entity, ProjectName!, "repository");
            Crawler.CreateFile(repositoryInterface);
            Crawler.WriteToFile(repositoryInterface, iRepoSkeleton!);

            string service = $"{entity}Service";
            string serviceSkeleton = Generator.Service(entity, ProjectName!);
            Crawler.CreateFile(service);
            Crawler.WriteToFile(service, serviceSkeleton);

            string repository = $"{entity}Repository";
            string repositorySkeleton = Generator.Repository(entity, ProjectName!);
            Crawler.CreateFile(repository);
            Crawler.WriteToFile(repository, repositorySkeleton);

            Crawler.MoveOut();
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeatureFiles: " + e.Message);
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


    private void BuildMvcFolders()
    {
        try
        {
            // Create
            // Move in
        }
        catch (Exception e)
        {
            throw new Exception("BuildFeatureFiles: " + e.Message);
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
            Crawler.CreateFile(file);

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

            Crawler.DeleteFile(file);
            Crawler.CreateFile(file);

            var programSkeleton = Generator.Program(Entities!, ProjectName!);
            Crawler.WriteToFile(file, programSkeleton);
        }
        catch (Exception e)
        {
            throw new Exception("CreateDbContext: " + e.Message);
        }
    }
}
