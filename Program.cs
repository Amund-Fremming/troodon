using troodon.Core;
using troodon.Cli;

namespace troodon;

public class Program
{
    static async Task Main(String[] args)
    {
        Orchestrator orchestrator = new Orchestrator();
        await orchestrator.Build();
    }
}
