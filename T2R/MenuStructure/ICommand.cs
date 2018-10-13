
namespace T2R.MenuStructure
{
    public interface ICommand   // de interface die gebruikt wordt om de elementen die tot het menu behoren toe te voegen
    {
        void RunCommand();
        string getName();
    }

    public class RunNewGame : ICommand
    {
        private string name;

        public string getName()
        {
            return name;
        }

        public RunNewGame(string name)
        {
            this.name = name;
        }

        public void RunCommand()
        {
            //Game game = new Game();
        }
    }

    public class RunExistingGame : ICommand
    {
        private string name;


        public string getName()
        {
            return name;
        }

        public RunExistingGame(string name)
        {
            this.name = name;
        }

        public void RunCommand()
        {
          //  Game game = new Game(false);
        }
    }
}
