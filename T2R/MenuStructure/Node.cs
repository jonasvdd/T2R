using System.Collections.Generic;


namespace T2R.MenuStructure
{
    public class Node           // wordt gebruikt voor de boomstructuur van het menu aan te amken
    {
        /*InstantieVariabelen*/
        private bool isRoot;
        List<Node> children = new List<Node>();
        Node parent;
        ICommand data;
        // de dataelementen zijn van het type iCommand


        /*Constructor*/
        // Creates a standard node
        public Node(Node pParent, ICommand pData)
        {
            parent = pParent;
            isRoot = false;
            data = pData;
            setParentChildren();
        }

        // Creates a root
        public Node(Menunode pMenu)
        {
            parent = null;
            isRoot = true;
            data = pMenu;
            setParentChildren();
        }

        public Node() { } // dit is de expliciete default constructor voor de Root Node
        // zo kan ik recursief accesmenu oproepen (anders moet ik mijn nodes in een lijst steken en oproepen via een extra methode)
        // wat ik oorspronkelijk gedaan had ... 


        /*Methods*/
        private void setParentChildren()
        {
            if (!isRoot)
            {
                parent.addChild(this);
            }
        }

        private void addChild(Node child)
        {
            children.Add(child);
        }


        /*properties*/
        public ICommand getData()
        {
            return data;
        }

        public List<Node> getChildNodes()
        {
            return children;
        }

        public Node getParent()
        {
            return parent;
        }
    }
}
