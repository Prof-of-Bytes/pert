namespace PERTCLI;

using System.Reflection;
using PertIO;
using PERTLib;
using PERTLib.MenuItem;

class Program
{
    static void Main(string[] args)
    {
        PertRunner run = new PertRunner();

    }
}
public class PertRunner
{

    public Dictionary<string, PertConsoleScreen> Actions;

    public PertRunner()
    {
        Actions = new Dictionary<string, PertConsoleScreen>
        {
            { "Main", PertConsoleScreen.NewMainScreen("Main") }
        };
        PertConsoleScreen.NewSubScreen("main sub 1", Actions["Main"]);
        PertConsoleScreen.NewSubScreen("main sub 2", Actions["Main"]);
        PertConsoleScreen.NewSubScreen("main sub 3", Actions["Main"]);
        PertConsoleScreen.NewSubScreen("main sub 4", Actions["Main"]);
        PertConsoleScreen.NewSubScreen("main sub 5", Actions["Main"]);
    }

}
