using Spectre.Console;
using troodon.Core;

namespace troodon;

public class Program
{
    private static void Main(String[] args)
    {
        AnsiConsole.Write(
            new FigletText("troodon")
            .LeftJustified()
            .Color(Color.White));

        Orchestrator orchestrator = new();
        orchestrator.Build();
    }
}