using troodon.Core;
using Spectre.Console;

namespace troodon;

public class Program
{
    static void Main(String[] args)
    {
        AnsiConsole.Write(
            new FigletText("troodon")
            .LeftJustified()
            .Color(Color.White));

        Orchestrator orchestrator = new Orchestrator();
        orchestrator.Build();
    }
}
