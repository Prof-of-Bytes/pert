using System.Text;

///
/// PERT 
/// By James Vernon
/// 23-Apr-2025
namespace PERT.PertFactory;
/// <summary>
/// <c>enum ProblemType</c> is an Enumeration of problem types. 
/// </summary>
public enum ProblemType : Int32
{
    Unknown = 0,
    Programming = 42,
    Discussion = 2,
    Quizes = 3,
}
public enum ProgrammingLanguages
{
    None = 0,
    CSharp = 1,
    Java = 2,
    BASH = 3
}
/// <summary>
/// IProblem is the interface to implement problems. Commmon: ID, Name, Type
/// </summary>
public interface IProblem
{
    Int32 ProblemID { get; }
    string ProblemName { get; }
    ProblemType ProblemType { get; }
}
/// <summary>
/// Abstract class Problem as the base class for concrete classes
/// </summary>
public abstract class Problem : IProblem, IEquatable<Problem>
{
    private Int32 _problemID;
    private string _problemName;
    private ProblemType _problemType;

    /// <summary>
    /// Per design guides base class is <c>protected</c>
    /// </summary>
    /// <param name="name">Name of Problem</param>
    /// <param name="ptype">Enum of a ProblemType</param>
    protected Problem(string name, ProblemType ptype)
    {
        _problemID = DateTime.Now.GetHashCode();
        _problemName = name;
        _problemType = ptype;
    }
    public int ProblemID => _problemID;
    public string ProblemName => _problemName;
    public ProblemType ProblemType => _problemType;
    /// <summary>
    /// Equals checks the _problemId field for value
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Problem? other)
    {
        if (other is not null && other.ProblemID == _problemID)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
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
        sb.AppendJoin('\t', ReadProblem());
        return sb.ToString();
    }

    public Tuple<string, string, string, List<string>, List<String>> ReadProblem()
    {
        return Tuple.Create(Name, ExpectedInput, ExpectedOutpt, Concepts, Hints);

    }
}