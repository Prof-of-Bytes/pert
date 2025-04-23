
namespace PERT.Problems
{
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
            {
                if (other.ProblemID == _problemID)
                {
                    return true;
                }
                return false;
            }

        }
    }
    /// <summary>
    /// Programming Languages is enumerated for indexing
    /// </summary>
    public enum ProgrammingLanguages
    {
        None = 0,
        CSharp = 1,
        Java = 2,

        BASH = 3
    }

    public class ProgrammingProblem : Problem
    {

        private const ProblemType _thisProblemType = ProblemType.Programming;
        private readonly ProgrammingLanguages _language;
        private string _expectedInput = string.Empty;
        private string _expectedOutput = string.Empty;
        private string[] _concepts;
        private string[] _hints;
        private string[] _requiredConcepts;
        ///<summary>
        ///Creates a new Programming Problem by passsing a name and language.
        ///ProblemType and Programming Language should be set at object creation
        ///A programming problem current contains the following attributes
        /// <list type="number">
        ///     <item>
        ///         <term>Expected Input</term>
        ///         <description>String description of problem inputs</description>
        ///     </item>
        ///     <item>
        ///         <term>Expected Output</term>
        ///         <description>String description of problem outputs</description>
        ///     </item>
        ///       <item>
        ///         <term>Concepts/term>
        ///         <description>string array of tokenized terms</description>
        ///     </item>
        ///       <item>
        ///         <term>Hints/term>
        ///         <description>string array of tokenized terms</description>
        ///     </item>
        ///       <item>
        ///         <term>required concepts</term>
        ///         <description>string array of tokenized terms</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="name">Problem Name try to be unique</param>
        /// <param name="lang">static methods can create problems or an enum can be passed</param>
        public ProgrammingProblem(string name, ProgrammingLanguages lang) : base(name, _thisProblemType)
        {
            _language = lang;
            _concepts = new string[5];
            _hints = new string[5];
            _requiredConcepts = new string[5];


        }
        /// <summary>
        /// Static method can be called by <c>PERT.Problems.ProgrammingProblem.CSharpProblem(string title)</c>
        /// </summary>
        /// <param name="title">Name of problem</param>
        /// <returns>new ProgrammingProblem with CSharp as Language</returns>
        protected static ProgrammingProblem CSharpProblem(string title)
        {
            return new ProgrammingProblem(title, ProgrammingLanguages.CSharp);
        }
        /// <summary>
        /// Use of tuple to return objects
        /// </summary>
        /// <returns>Tuple, int: problem id, string: name, string: type, string: language, string: input, string: output </returns>
        public Tuple<int, string, string, string, string, string> ProblemInfo()
        {
            //ID, title, type, lang, input, output, concepts[], hints[], requirements[]
            return Tuple.Create(ProblemID, ProblemName, ProblemType, _language.ToString(), _expectedInput, _expectedOutput);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>string array concepts</returns>
        public string[] GetConcepts()
        {
            return _concepts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>string array hints</returns>
        public string[] GetHints()
        {
            return _hints;
        }

    }
}