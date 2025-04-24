///
/// PERT 
/// By James Vernon
/// 23-Apr-2025
using System.IO;
using System.Text;


namespace PERT.ReadWrite;

public struct PERTInfo
{
    public int ProblemId { get; }
    public string ProblemType { get; }
    public ProgrammingProblem Problem { get; set; }
}
public interface IPERTReader
{

    public StreamReader Reader { get; set; }
    public PERTInfo ReadProblem();
    public List<PERTInfo> ReadAllProblems();
}
public interface IPERTWriter
{
    public StreamWriter Writer { get; set; }
    public void WriteProblem(ProgrammingProblem problem);
    public void WriteAllProblems(List<ProgrammingProblem> problemList)
}

public class PERTConsoleWriter : IPERTWriter
{
    public StreamWriter Writer { get; set; }
    public PERTConsoleWriter()
    {
        Writer = System.Console.Out;
    }

    public void WriteProblem(ProgrammingProblem problem)
    {
        Writer.WriteLine(problem.ToString());
    }
    public void WriteAllProblems(List<ProgrammingProblem> problemList)
    {
        foreach (ProgrammingProblem problem in problemList)
        {
            Writer.WriteLine(problem.ToString());
        }
    }
    static void WriteText(string text)
    {
        Writer.WriteLine(text);
    }

}

public class PERTFileWriter : IPERTWriter
{
    public static string WorkingDirectory => Directory.GetCurrentDirectory();
    public StreamWriter Writer { get; init; }
    public string FilePath { get; init; }
    public FileStream FileStream { get; init; }
    public PERTFileWriter(string fileName)
    {
        try
        {
            FilePath = $"{WorkingDirectory}/{fileName}.txt";
            FileStream = new FileStream(FilePath, new FiLeStreamOptions());
            Writer = new StreamWriter(FileStream);
        }
        catch (IOException e)
        {

        }
    }
    public bool WriteProblem(ProgrammingProblem problem)
    {

    }

    public bool WriteAllProblems(List<ProgrammingProblem> problems)
    {

    }
}

