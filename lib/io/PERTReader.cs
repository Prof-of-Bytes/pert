///
/// PERT 
/// By James Vernon
/// 23-Apr-2025
using System.Dynamic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PERT.PertFactory;

namespace PERT.ReadWrite;




public interface IRead
{
    public StreamReader Reader { get; }
    public string DataFilePath { get; set; }
    public HashSet<ProgrammingProblem> WorkingSet { get; set; }

}
public abstract class PERTReader : IRead
{
    public StreamReader Reader { get; set; }
    public string DataFilePath { get; set; }
    public HashSet<ProgrammingProblem> WorkingSet { get; set; }
    public PERTReader(StreamReader reader, string filePath, HashSet<ProgrammingProblem> workingSet)
    {
        if (reader == null)
        {
            throw new NullReferenceException();
        }
        else
        {
            Reader = reader;
        }
        DataFilePath = filePath ?? string.Empty;
        WorkingSet = workingSet ?? [];
    }
    public abstract string ReadLine();
    public abstract string ReadAllLines();

}

public class PERTFileReader : PERTReader
{
    public PERTFileReader(StreamReader reader, string filePath, HashSet<ProgrammingProblem> workingSet) : base(reader, filePath, workingSet)
    {

    }
    private StreamReader Open()
    {
        try
        {
            return Reader = new StreamReader(DataFilePath);
        }
        catch (FileNotFoundException)
        {
            return Reader = new StreamReader("default_directory.txt");
        }
    }
    public override string ReadAllLines()
    {
        throw new NotImplementedException();
    }
    public override string ReadLine()
    {
        throw new NotImplementedException();
    }
}
