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
        if (other == null)
        {
            return false;
        }
        if (other.ProblemID == _problemID)
        {
            return true;
        }
    }
}
