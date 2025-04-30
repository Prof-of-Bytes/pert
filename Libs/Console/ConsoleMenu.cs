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
namespace PERT.ConsoleMenu
{
    /// <summary>
    /// Flagged enum for controlling program behavior
    /// </summary>
    [Flags]
    public enum PROGAM_STATE
    {

        NOTHING = 0b_0000_0000,
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
    /// <summary>
    /// IMenuItem contains the needed data for creating a list of user choices
    /// </summary>
    public interface IMenuItem
    {
        //important to track to control program behavior
        public PROGAM_STATE CurrentState { get; }
        public string Name { get; }
        public bool IsSubMenu { get; }
        /// <summary>
        /// SubScreens is a Stack data structre for a LIFO approach
        /// </summary>
        public Stack<IMenuItem> SubScreens { get; }
        public IMenuItem? ParentItem { get; }
    }
    public abstract class ConsoleMenuItem : IMenuItem
    {
        private TextReader _in;
        private TextWriter _out;
        private TextWriter _err;
        private PROGAM_STATE _state = PROGAM_STATE.NOTHING;
        private string _name = string.Empty;
        private Stack<IMenuItem> _subScreens;
        private IMenuItem _parent;
        /// <summary>
        ///  This abstract class will allow us to contorl the reader and writer of the console as well as a default file location
        /// </summary>
        /// <param name="reader">In </param>
        /// <param name="writer">Out</param>
        /// <param name="err">Err</param>
        /// <param name="name">Menu Screen Name</param>
        /// <param name="parent">Parent object for ref</param>
        protected ConsoleMenuItem(TextReader reader, TextWriter writer, TextWriter err, string name, IMenuItem? parent)
        {
            _in = reader;
            _out = writer;
            _err = err;
            _subScreens = new Stack<IMenuItem>();
            _name = name;
            _parent = parent!;
        }
        /// <summary>
        /// consturctor using object
        /// </summary>
        /// <param name="screen"></param>
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
    /// <summary>
    /// PERT Console
    /// </summary>
    public class PertConsoleScreen : ConsoleMenuItem
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Screen menu item</param>
        /// <param name="parent">ref to parent</param>
        /// <param name"base">Base object construcor uses Console.In|Out|Error</param>
        public PertConsoleScreen(string name, ConsoleMenuItem? parent) : base(Console.In, Console.Out, Console.Error, name, parent)//Console passesd here
        {
        
        }
        /// <summary>
        /// static method to create a main screen
        /// </summary>
        /// <param name="name">main screen name</param>
        /// <returns>PertConsoleScreen with name</returns>
        public static PertConsoleScreen NewMainScreen(string name) => new(name, null);
        /// <summary>
        /// Creates a new sub screen with parent set to this object
        /// </summary>
        /// <param name="name">sub screen name</param>
        public void NewSubScreen(string name)
        {
            this.AddScreen(new PertConsoleScreen(name, this));
        }
        /// <summary>
        /// Constuructor using screen
        /// </summary>
        /// <param name="screen"></param>
        public PertConsoleScreen(ConsoleMenuItem screen) : base(screen) { }
        /// <summary>
        /// private method to clean the console screen
        /// </summary>
        private static void Clean()
        {   
            //clear
            Console.Clear();
            ///windows only method...
            try
            {

                Console.MoveBufferArea(0, 0, 120, 120, 0, 0, ' ', ConsoleColor.DarkGreen, ConsoleColor.Black);
            }
            catch (PlatformNotSupportedException)
            {
                Console.WriteLine("Guess Your Platform is not supported...");
            }
        }
        /// <summary>
        /// Writes a menu screen using string builder
        /// </summary>
        public void WriteMenu()
        {
            StringBuilder bld = new();
            bld.Append('#', 120).AppendLine();
            bld.AppendLine($"{Name.PadLeft(10)}");

            bld.AppendLine("Options".PadLeft(20));

            int i = 0;
            foreach (IMenuItem sub in this.SubScreens)
            {
                bld.AppendLine($"{i++} --- {sub.Name}");
            }
            bld.Append('#', 120).AppendLine();
            WriteToConsole(bld.ToString());

        }
        /// <summary>
        /// Add a sub screen to this object... 
        /// </summary>
        /// <param name="sub">PertConsole Screen added to the Stack </param>
        private void AddScreen(PertConsoleScreen sub)
        {
            SubScreens.Push(sub);
        }
        /// <summary>
        /// Writes text out
        /// </summary>
        /// <param name="text"></param>
        public void WriteToConsole(string text)
        {
            Out.WriteLine(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new line from the reader or if empty program error</returns>
        public string ReadFromConsole()
        {
            return In.ReadLine() ?? PROGAM_STATE.NO_CONSOLE_INPUT.ToString();
        }
    }