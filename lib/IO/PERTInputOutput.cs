/**
PERT 
By James Vernon
23-Apr-2025
<summary>
    PERT is the tool used to create and store programming problems. 
    Namespace <c>PERT.Problems</c> contains the entites for creating
    Problems abstract and Programming Problems as a type of Problem.
</summary>
*/
namespace PERT.PERTInputOutput
{
    /// <summary>
    /// PERT Stream contains data file locations and stream handlers to read and write from terminal to disk
    /// </summary>
    public class PertStream
    {

        public TextReader PERTIn { get; init; }
        public TextWriter PERTOut { get; init; }

        public TextWriter PERTErr { get; init; }

        public string DataFileLocation { get; init; }

        /// <summary>
        /// Cereate a new TextReader, TextWriter for in, out, and err. path is the file location
        /// </summary>
        /// <param name="inStream">TextReader in</param>
        /// <param name="outStream">TextWriter out</param>
        /// <param name="errStream">TextWrtiter err</param>
        /// <param name="path">file path string value...</param>
        public PertStream(TextReader inStream, TextWriter outStream, TextWriter errStream, string path)
        {

            DataFileLocation = Path.GetFullPath(path);
            PERTIn = inStream ?? new StreamReader(DataFileLocation);
            PERTOut = outStream ?? new StreamWriter(DataFileLocation);
            PERTErr = errStream ?? new StreamWriter(DataFileLocation);

        }
        public void TestStreams()
        {
            PERTOut.WriteLine(DataFileLocation + " Called FROM PERT IO");
        }
    }
}