using troodon.Core;
using troodon.Cli;

namespace troodon;

public class Program
{
    static void Main(String[] args)
    {
        Orchestrator orchestrator = new Orchestrator();
        orchestrator.Build();
    }
}
