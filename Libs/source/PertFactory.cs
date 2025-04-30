using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;
using PERT.Libs.ReadWrite;


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
    READY = 0b_0000_0000,
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
public interface IMenuItem
{
    //important to track to control program behavior
    public string Name { get; set; }

}
public abstract class AbstractMenu : IMenuItem
{
    public string Name { get; set; }
    public PROGAM_STATE CurrentState { get; set; }
    public IPERTReadWrite PERTStream { get; set; }
    public AbstractMenu(string name, IPERTReadWrite rwObject)
    {
        Name = name;
        CurrentState = PROGAM_STATE.READY;
        PERTStream = rwObject;
    }
}

public class ConsoleMenu(string name) : AbstractMenu(name, new PERTConsoleRW((StreamReader)Console.In, (StreamWriter)Console.Out))
{

}


