namespace PERTCLI;

using PertIO;
using PERTLib;
class Program
{    static void Main(string[] args)
    {
        PertStream ps = new(Console.In, Console.Out, Console.Error, "test-file.txt");
        ps.TestStreams();
    }
}
