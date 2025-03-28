using Microsoft.VisualBasic;

namespace PERTLib.Structures
{
    public interface IProblem
    {
        Int32 ProblemID { get; }
        string ProblemName { get; }

        string ProblemType { get; }

    }
    public enum ProblemType : Int32
    {
        Unknown = 0,
        Programming = 42,

        Discussion = 2,

    }


    public abstract class Problem : IProblem, IEquatable<Problem>
    {
        private Int32 _problemID;
        private string _problemName;
        private ProblemType _problemType;


        protected Problem(string title, ProblemType ptype)
        {
            _problemID = DateTime.Now.GetHashCode();
            _problemName = title;
            _problemType = ptype;
        }
        public int ProblemID => _problemID;

        public string ProblemName => _problemName;

        public string ProblemType => _problemType.ToString();

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
    public enum ProgrammingLanguages
    {
        None = 0,
        CSharp = 1,
    }

    public class ProgrammingProblem : Problem
    {
        private const ProblemType _thisProblemType = Structures.ProblemType.Programming;
        private readonly ProgrammingLanguages _language;

        private string _expectedInput = string.Empty;
        private string _expectedOutput = string.Empty;
        private string[] _concepts;
        private string[] _hints;

        private string[] _requiredConcept;



        public ProgrammingProblem(string name, ProgrammingLanguages lang) : base(name, _thisProblemType)
        {
            _language = lang;
            _concepts = new string[5];
            _hints = new string[5];
            _requiredConcept = new string[5];


        }
        protected static ProgrammingProblem CSharpProblem(string title)
        {
            return new ProgrammingProblem(title, ProgrammingLanguages.CSharp);
        }
        public Tuple<int, string, string, string, string, string> ProblemInfo()
        {
            //ID, title, type, lang, input, output, concepts[], hints[], requirements[]
            return Tuple.Create(ProblemID, ProblemName, ProblemType, _language.ToString(), _expectedInput, _expectedOutput);
        }
        public string[] GetConcepts(){
            return _concepts;
        }
        public string[] GetHints(){
            return _hints;
        }

    }


}
