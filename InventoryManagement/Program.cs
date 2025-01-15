using InventoryManagement.Inventory;
class Program
{
    static void Main()
    {
        Console.WriteLine($"====== Inventory Manager =======");
        InventoryManager manager = new InventoryManager();
        ConsoleUI inventory = new ConsoleUI(manager);
        bool isRunning = true;
        while (isRunning)
        {
            try
            {
                UserChoice userChoice = (UserChoice)inventory.ShowMenu();
                switch (userChoice)
                {
                    case UserChoice.Add:
                        inventory.AddProduct();
                        break;
                    case UserChoice.Display:
                        inventory.ShowProducts();
                        break;
                    case UserChoice.Delete:
                        inventory.DeleteProduct();
                        break;
                    case UserChoice.Edit:
                        inventory.EditProduct();
                        break;
                    case UserChoice.Search:
                        inventory.SearchProduct();
                        break;
                    case UserChoice.SortedDisplay:
                        inventory.SortedView();
                        break;
                    case UserChoice.Exit:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("[-] That Feature is not available Yet !! ");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[-] Enter a Valid Choice as numbber ");
            }
        }
        Console.WriteLine($"Thank You !!");
            
    }
}




