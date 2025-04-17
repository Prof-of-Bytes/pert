using System.Collections;
using System.Net;
using System.Reflection.Metadata;


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
    public List<IMenuItem> PossibleActions { get; }
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
    private bool _subMenu;
    private Stack<ConsoleMenuItem> _subScreens;

    private IMenuItem _parent;

    protected ConsoleMenuItem(TextReader reader, TextWriter writer, TextWriter err, string name, IMenuItem? parent)
    {
        _in = reader;
        _out = writer;
        _err = err;
        _name = name;
        _subScreens = new Stack<ConsoleMenuItem> ();
        _parent = parent!;
    }
    protected ConsoleMenuItem(ConsoleMenuItem screen) : this(screen._in, screen._out, screen._err, screen.Name, screen.ParentItem) { }

    public PROGAM_STATE CurrentState => _state;

    public string Name => _name;

    public bool IsSubMenu => _parent is null;
   
    public Stack<ConsoleMenuItem> SubScreens => _subScreens;

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

    public ConsoleMenuItem NewMainScreen(string name) => new PertConsoleScreen(name, null);

    public ConsoleMenuItem NewSubScreen(string name, ConsoleMenuItem parent) => new PertConsoleScreen(name, parent);

    public PertConsoleScreen(ConsoleMenuItem screen) : base(screen) { }

    public void AddSubScreen(ConsoleMenuItem screen )
    {
       
    }
}








