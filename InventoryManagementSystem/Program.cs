namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserInterface userInterface = new ConsoleUI();
            userInterface.Display();
        }
    }
}