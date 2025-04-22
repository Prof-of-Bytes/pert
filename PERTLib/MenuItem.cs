using System.Collections;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Text;
using Microsoft.VisualBasic;


namespace PERTLib.MenuItem;

/*
    Possible Program States
    File to Store Text
    Text From That File

    -- File: Text|!Text
    -- Data File|!File
    -- No Data

*/
[Flags]
public enum PROGAM_STATE
{

    NOTHING = 0b_0000_0000,
    DATA_STORE = 0b_0000_0001, //A file properly config
    DATA = 0b_0000_1000, //data in the file or data ready to write

    NO_CONSOLE_INPUT = 0b_1110_0000,
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

    public PROGAM_STATE CurrentState { get; }
    public string Name { get; }
    public bool IsSubMenu { get; }
    //Possible Actions would represent from where the menu is at to where it could
    public Stack<IMenuItem> SubScreens { get; }
    public IMenuItem? ParentItem { get; }


}

public abstract class ConsoleMenuItem : IMenuItem
{
    private TextReader _in;
    private TextWriter _out;

    private TextWriter _err;

    private const string DASHTAB = "\t----------------\t";

    private PROGAM_STATE _state = PROGAM_STATE.NOTHING;
    private string _name = string.Empty;
    private Stack<IMenuItem> _subScreens;

    private IMenuItem _parent;

    protected ConsoleMenuItem(TextReader reader, TextWriter writer, TextWriter err, string name, IMenuItem? parent)
    {
        _in = reader;
        _out = writer;
        _err = err;
        _subScreens = new Stack<IMenuItem>();
        _name = name;
        _parent = parent!;
    }
    protected ConsoleMenuItem(ConsoleMenuItem screen) : this(screen._in, screen._out, screen._err, screen.Name, screen.ParentItem) { }

    public PROGAM_STATE CurrentState => _state;

    public string Name => _name;

    public bool IsSubMenu => _parent is null;

    public Stack<IMenuItem> SubScreens => _subScreens;
    
       
    public IMenuItem ParentItem => _parent;

    public TextWriter Out => _out;

    public TextWriter Err => _err;

    public TextReader In => _in;
}

public class PertConsoleScreen : ConsoleMenuItem
{
    public PertConsoleScreen(string name, ConsoleMenuItem? parent) : base(Console.In, Console.Out, Console.Error, name, parent)
    {
       
    }

    public static PertConsoleScreen NewMainScreen(string name) => new(name, null);

    public void  NewSubScreen(string name)
    {
      this.AddScreen(new PertConsoleScreen(name, this));  
    }

    public PertConsoleScreen(ConsoleMenuItem screen) : base(screen) { }

    private static void Clean()
    {
        Console.Clear();
        try
        {

            Console.MoveBufferArea(0, 0, 120, 120, 0, 0, ' ', ConsoleColor.DarkGreen, ConsoleColor.Black);
        }
        catch (PlatformNotSupportedException)
        {
            Console.WriteLine("Guess Your Platform is not supported...");
        }
    }
   public void WriteMenu()
    {
        StringBuilder bld = new ();
        bld.Append('#', 120).AppendLine();
        bld.AppendLine($"{Name.PadLeft(10)}");
        
        bld.AppendLine("Options".PadLeft(20));
       
        int i = 0;
        foreach(IMenuItem sub in this.SubScreens)
        {
            bld.AppendLine($"{i++} --- {sub.Name}");
        }
        bld.Append('#', 120).AppendLine();
        WriteToConsole(bld.ToString());
        
    }
    private void AddScreen(PertConsoleScreen sub){
        SubScreens.Push(sub);
    }

    public void WriteToConsole(string text)
    {
        Out.WriteLine(text);
    }
    public string ReadFromConsole()
    {
        return In.ReadLine() ?? PROGAM_STATE.NO_CONSOLE_INPUT.ToString();
    }


}









