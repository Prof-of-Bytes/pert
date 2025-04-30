///
/// PERT 
/// By James Vernon
/// 23-Apr-2025

using System.Text;
using System.Threading.Tasks.Dataflow;
using PERT.PertFactory;

namespace PERT.Libs.ReadWrite;

public interface IPERTReadWrite
{
    public static Stream? RWStream { get; set; }
    public string DataFilePath { get; set; }
    public HashSet<ProgrammingProblem> WorkingSet { get; set; }

}
public abstract class PERTAbstractReaderWriter : IPERTReadWrite
{
    public static Stream? RWStream { get; set; }
    public string DataFilePath { get; set; }
    public HashSet<ProgrammingProblem> WorkingSet { get; set; }
    public PERTAbstractReaderWriter(Stream stream, string? filePath, HashSet<ProgrammingProblem>? workingSet)
    {

        if (stream == null)
        {
            throw new NullReferenceException();
        }
        else
        {
            RWStream = stream;
        }

        DataFilePath = filePath ?? string.Empty;
        WorkingSet = workingSet ?? [];
    }
    public abstract string ReadLine();
    public abstract string ReadAllLines();
    public abstract void Write(string text);

}

public class PERTConsoleRW : PERTAbstractReaderWriter
{
    public StreamReader In { get; set; }
    public StreamWriter Out { get; set; }
    public bool RWError = false;
    public string ErrorMessage = string.Empty;
    public Exception ExceptionState { get; set; } = null!;
    private PERTConsoleRW(Stream stream, string? filePath, HashSet<ProgrammingProblem>? workingSet) : base(stream, filePath, workingSet)
    {
        In = new StreamReader(stream);
        Out = new StreamWriter(stream);
    }
    public PERTConsoleRW(StreamReader inStream, StreamWriter outStream) : this(inStream.BaseStream, null, null) { }

    public override string ReadAllLines()
    {
        if (In is null)
        {
            throw new NullReferenceException();
        }
        try
        {
            return In.ReadToEnd();
        }
        catch (Exception error)
        {
            RWError = true;
            ErrorMessage = error.Message;
            ExceptionState = error;
        }
        return string.Empty;
    }
    public override string ReadLine()
    {
        if (In is null)
        {
            throw new NullReferenceException();
        }
        else
        {
            try
            {
                return In.ReadLine() ?? string.Empty;
            }
            catch (Exception error)
            {
                RWError = true;
                ErrorMessage = error.Message;
                ExceptionState = error;
            }
        }
        return string.Empty;

    }
    public override void Write(string line)
    {
        if (Out is null)
        {
            throw new NullReferenceException();
        }
        else
        {
            try
            {
                Out.Write(line);
            }
            catch (Exception error)
            {
                RWError = true;
                ErrorMessage = error.Message;
                ExceptionState = error;
            }
        }

    }

}