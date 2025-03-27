namespace PERTLib.Structures;

public interface IProblem
{
    int ProblemId { get; }
    string Title { get; }

    string Description { get; }

    string[] Concepts { get; }

    string[] Hints { get; }


}

public abstract class Problem : IProblem
{
    private int _pid;
    private string _title;
    private string _description;
    private string[] _concepts;
    private string[] _hints;
    public Problem(int pid, string title, string description, string[] concepts, string[] hints)
    {
        _pid = pid;
        _title = title;
        _description = description;
        _concepts = concepts;
        _hints = hints;
    }

    public int ProblemId => _pid;
    public string Title => _title;
    public string Description => _description;
    public string[] Concepts => _concepts;
    public string[] Hints => _hints;

}

public enum LanguageType
{
    UNDEFINED = -1,
    C = 1,
    CSHARP = 2,
    BASH = 3,
    JAVA = 4,
    PYTHON = 5,
    GO = 6,



}

public enum ChallengeLevel
{

    BEG,
    INT,
    ADV,
    MIX
}

public struct LangProbInfo
{
    public LangProbInfo(int pid, LanguageType lang)
    {
        ProblemId = pid;
        Language = lang;
    }
    public readonly int ProblemId;
    public readonly LanguageType Language;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string[]? Concepts { get; set; }
    public string[]? Hints { get; set; }
    public ChallengeLevel? Level { get; set; }

    public string? ExpectedInput { get; set; }

    public string? ExpectedOutput { get; set; }

}

public class ProgammingProblem : Problem
{
    private LangProbInfo _problemInfo;
    public ProgammingProblem(LangProbInfo problem) : base(problem.ProblemId, problem.Title, problem.Description, problem.Concepts, problem.Hints)
    {
        _problemInfo = problem;
        switch(problem)
        {
            case Title
                _problemInfo.Title = "Awaiting Title";
                break;
            case Description == null:
                _problemInfo.Description = "Missing";
                break;
            case Concepts == null:
            case Hints == null:
            default:
        }
    }
}

