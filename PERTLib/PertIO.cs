
namespace PertIO;

public class PertStream
{

    public  TextReader PERTIn { get; init; }
    public  TextWriter PERTOut { get; init; }

    public  TextWriter PERTErr { get; init;}

    public  string DataFileLocation {get; init;}


    public PertStream(TextReader inStream, TextWriter outStream, TextWriter errStream, string path){
        
        DataFileLocation = Path.GetFullPath(path);
        PERTIn = inStream ?? new StreamReader(DataFileLocation);
        PERTOut  = outStream ?? new StreamWriter(DataFileLocation);
        PERTErr = errStream ?? new StreamWriter(DataFileLocation);

    }
    public void TestStreams(){
        PERTOut.WriteLine(DataFileLocation + " Called FROM PERT IO");
    }
}

