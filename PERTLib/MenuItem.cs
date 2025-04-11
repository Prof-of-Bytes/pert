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

    public abstract class MenuItem : IMenuItem
    {
        private PROGAM_STATE _state = PROGAM_STATE.NOTHING;
        private string _name = string.Empty;
        private bool _subMenu;
        private List<IMenuItem> _actions;

        private IMenuItem _parent;

        protected MenuItem(string name, bool sub, IMenuItem? parent)
        {
            _name = name;
            _subMenu = sub;
            _actions = [];
            _parent = parent!;
        }

        public PROGAM_STATE CurrentState => _state;

        public string Name => _name;

        public bool IsSubMenu => _subMenu;

        public List<IMenuItem> PossibleActions => _actions;

        public IMenuItem ParentItem => _parent;
    }


    public class ConsoleMenuItem : MenuItem
    {

        private TextReader _in;
        private TextWriter _out;

        const string DASHTAB = "\t----------------\t";

        public ConsoleMenuItem(TextReader reader, TextWriter writer, string name, bool sub, IMenuItem? parent) : base(name, sub, parent)
        {
            _in = reader;
            _out = writer;
        }
        public void PrintName()
        {
            _out.WriteLine($"{DASHTAB}**{Name}**{DASHTAB}");
        }

    }









