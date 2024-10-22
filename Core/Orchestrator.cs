using troodon.Cli;
using Spectre.Console;

namespace troodon.Core;

public class Orchestrator
{
    private string? ProjectName;
    private int NumberOfEntities;
    private IList<string>? Entities;
    private string? Database;

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
            Database = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What kind of [blue]Database[/]?")
                .AddChoices("In memory", "I have a connection string"));
        }
        catch (Exception e)
        {
            throw new Exception("GetProjectData: " + e.Message);
        }
    }

    public void Build()
    {

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arrow)
            .SpinnerStyle(Style.Parse("blue"))
            .Start("Generating project", ctx =>
            {
                Executor.BuildDotnetBase(ProjectName!);

                ctx.Status("Restoring NuGets");
                Executor.FetchEfNuGets(ProjectName!);

                ctx.Status("Generating architecture");
                Crawler.MoveIn(ProjectName!);
                Crawler.CreateDir("Features");

                BuildFeatures();
                BuildDbContext();

                if (Database!.Equals("In memory"))
                {
                    BuildProgramCs(true);
                    ctx.Status("Building in memory db");
                    Thread.Sleep(1000);
                }

                if (Database.Equals("I have a connection string"))
                {
                    BuildProgramCs(false);
                    ctx.Status("Building db");
                    Thread.Sleep(1000);
                    // BuildAppSettings()
                    Executor.RunMigration(ProjectName!);
                }
            });
    }

    private void BuildAppSettings()
    {
        try
        {
            string skeleton = Generator.AppSettings(connectionString);
            string appsettings = "appsettings.json";
            Crawler.DeleteFile(appsettings);
            Crawler.CreateFile(appsettings);
            Crawler.WriteToFile(appsettings, skeleton);
        }
        catch (Exception e)
        {
            throw new Exception("BuildAppSettings: " + e.Message);
        }
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

    private void BuildProgramCs(bool inMemory)
    {
        try
        {
            var file = "Program";

            Crawler.DeleteFile(file);
            Crawler.CreateFile(file);

            var programSkeleton = Generator.Program(Entities!, ProjectName!, inMemory);
            Crawler.WriteToFile(file, programSkeleton);
        }
        catch (Exception e)
        {
            throw new Exception("CreateDbContext: " + e.Message);
        }
    }
}
