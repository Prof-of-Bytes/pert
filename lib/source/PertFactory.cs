using Microsoft.VisualBasic;


///
/// PERT 
/// By James Vernon
/// 23-Apr-2025
namespace PERT.PertFactory;
/// <summary>
/// Flagged enum for controlling program behavior
/// </summary>
[Flags]
public enum PROGAM_STATE
{
    NOTHING = 0b_0000_0000,
    DATA_STORE = 0b_0000_0001, //A file properly config
    DATA = 0b_0000_1000, //data in the file or data ready to write
    NO_CONSOLE_INPUT = 0b_1110_0000,//Flag for an action without input
    DATA_STORE_DATA_NOT_LOADED = DATA_STORE ^ DATA,
    DATA_NO_DATA_STORE = DATA ^ DATA_STORE,
    READ = 0b_1000_0000,
    WRITE = 0b_1100_0000,
    READ_WRITE = READ | WRITE,
    NO_DATA_TO_RW = READ_WRITE ^ DATA,
    NO_DATA_STORE_DATA_TO_RW = READ_WRITE ^ DATA_STORE
}
public enum ProgrammingLanguages
{
    None = 0,
    CSharp = 1,
    Java = 2,
    BASH = 3
}
public class ProgrammingProblem(string name, ProgrammingLanguages lang) : Problem(name, _thisProblemType)
{
    public const ProblemType _thisProblemType = ProblemType.Programming;
    public ProgrammingLanguages Lang { get; init; } = lang;
    public string Name { get; set; } = name;
    public string ExpectedInput { get; set; } = string.Empty;
    public string ExpectedOutpt { get; set; } = string.Empty;
    public List<string> Concepts { get; set; } = [];
    public List<string> Hints { get; set; } = [];
    /// <summary>
    /// id type language
    /// </summary>
    /// <returns>int, string, string</returns>
    public ValueTuple<int, string, string> ProblemInfo()
    {
        //ID, title, type, lang, input, output
        return (ProblemID, ProblemType.ToString(), this.Lang.ToString());
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendJoin('\t', ProblemInfo());
        sb.AppendJoin('\t', ReadProblem);
        return sb.ToString();
    }
}

/// <summary>
/// Creates a new CSharpProblem with name
/// </summary>
/// <param name="name">Name of problem</param>
public static class CSHarpProblem(string name) => new ProgrammingProblem(name, ProgrammingLanguages.CSharp)

public class PERTWorkspace
{
    private PERTReader reader;
    private PERTWriter writer;
    public PERTWorkspace()
    {

    }
    public OrderedDictionary<int, ProgrammingProblem> Problems => new();
    /// <summary>
    /// name, input, output, concepts, hints
    /// </summary>
    public Tuple<string, string, string, List<string>, List<string>> Workspace = new();
    public ProgrammingProblem GetProblem(int id)
    {
        return Problems[id];
    }
    public void AddProblem(ProgrammingProblem problem)
    {
        Problems.Add(problem);
    }
    public int ProblemCount => Problems.Count;
    public void SetName(string name) => Workspace.Item1 = name;
    public void SetExpectedInput(string input) => Workspace.Item2 = input;

    public void SetExpectedOutput(string output) => Workspace.Item3 = output;

    public void SetConcepts(List<string> concepts) => Workspace.Item4 = concepts;

    public void SetHints(List<string> hints) => Workspace.Item5 = hints;

    public string ReadTabInfo(ProgrammingProblem problem)
    {
        return problem.ToString();
    }
}