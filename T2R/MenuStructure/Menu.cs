using System;
using System.Collections.Generic;

namespace T2R.MenuStructure
{
    public class Menunode : ICommand    // vai deze klasse wordt een menu aangemaakt
    {
        string menuName;
        private List<ICommand> CommandList = new List<ICommand>();   // list that contains methods to execute all projects
        private List<Menunode> SubmenuList = new List<Menunode>();

        public Menunode(string name)
        {
            menuName = name;
        }

       
        public void RunCommand()
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            return menuName;
        }
    }
}
