using System;
using InventoryManagement.ProductDecorator;
namespace InventoryManagement.Inventory;

public class ConsoleUI
{   
    private readonly InventoryManager _manager;

    public ConsoleUI(InventoryManager manager)
    {
        _manager = manager;
    }
     public int ShowMenu()
    {
        Console.WriteLine("\nEnter the Operation You want to perform: " +
            "\n [1] - Add Product " +
            "\n [2] - Display Products " +
            "\n [3] - Delete Product " +
            "\n [4] - Update Product " +
            "\n [5] - Search " +
            "\n [6] - Sorted View" +
            "\n [7] - exit \n" );
        var userInput = Console.ReadLine();
        int userChoice;
        if (!int.TryParse(userInput, out userChoice))
        {
            return 0;
        }
        return userChoice;
    }

    public void AddProduct()
    {
        string name = GetName("Enter the Name of the Product : ");
        if(_manager.IsProductPresent(name))
            {
                if(!Restock(name))
                {
                    AddProduct();
                }
                return;
            }
        double price = GetPrice("Enter the price of the product : ");
        int quantity = GetQuantity("Enter the Quantity of the Product : ");
        IProduct nonPerishable = new Product(name,price,quantity);
        if(IsPerishable())
        {
            DateOnly expiryDate = GetExpiryDate($"Enter the ExpiryDate (e.g., yyyy-mm-dd) : ");
            IProduct perishable = new ExpiryDecorator(nonPerishable,expiryDate);
            _manager.Add(perishable);
            Console.WriteLine($"Product Added Successfully !!");
            
            return;
        }
        _manager.Add(nonPerishable);
        Console.WriteLine($"Product Added Successfully !!");
        return;
    }
    public void ShowProducts()
    {
        Console.WriteLine($"Your Inventory : \n");
        IList<string> products = _manager.DisplayProducts();
        if(products.Count > 0)
        {
             foreach(var product in products)
             {
                 Console.WriteLine(product);
             }
        }
        else
        {
             Console.WriteLine($"\tNo Products to Show !!");
        }
    }

    public void EditProduct()
    {
        string name = GetName("Enter the name of the product You want to update : ");
        if(_manager.IsProductPresent(name))
        {    
            string nameToUpdate = GetName($"Enter the new name for [{name}] : ");
            double priceToTpdate = GetPrice($"Enter the new price for [{name}] : ");
            int quantityToUpdate = GetQuantity($"Enter the new Quantity for [{name}] : ");
            if(_manager.IsPerishable(name))
            {
                DateOnly expiryDate = GetExpiryDate($"Enter the new expiry date for [{name}] (e.g., yyyy-mm-dd) : ");
                _manager.UpdateProduct(name,nameToUpdate,priceToTpdate,quantityToUpdate,expiryDate);
                return;
            }
            _manager.UpdateProduct(name,nameToUpdate,priceToTpdate,quantityToUpdate);
            Console.WriteLine($"Product [{name}] Updated Successfully !!");
        }
        Console.WriteLine($"No Product with name : {name} is found");
        return;
    }

    public void SortedView()
    {   
        Console.WriteLine($"Sorted Display :");
        IList<string> products = _manager.SortProducts();
        foreach(var product in products)
        {
            Console.WriteLine(product);
            
        }
    }

    public void SearchProduct()
    {   
        string name = GetName("Enter the name of the Product to search : ");
        Console.WriteLine($"\nSearched Product :");
        var product = _manager.SearchProduct(name);
        Console.WriteLine($"\t\t{product}");
        
    }

    public bool Restock(string name)
    {
        Console.Write("[*] Product with the same Name was Found. " +
            "\nDo You want to Restock ? [Yes/No] : ");
        var userChoice = Console.ReadLine();
        if(userChoice.Equals("yes",StringComparison.OrdinalIgnoreCase))
        {
            int quantity = GetQuantity("Enter the Quantity to be Restocked :");
            _manager.RestockProduct(name,quantity);
            Console.WriteLine($"{name} : Restocked for {quantity}");
            return true;
        }
        else
        {
            Console.WriteLine("[-] Then Provide a Unique Product Name or include Brand Name !!");
            return false;
        }
    }
    public void DeleteProduct()
    {
        string name = GetName("Enter the name of the product you Want to Delete : ");
        if(_manager.IsProductPresent(name))
        {
            _manager.RemoveProduct(name);
            Console.WriteLine($"Product [{name}] Deleted Successfully !!");
            return;
        }
        Console.WriteLine($"No Product with name : {name} is found");  
    }

    private bool IsPerishable()
    {
        Console.Write($"Is this product Perishable ? [yes / no] : ");
        bool isPersishable = Console.ReadLine()?.Trim().ToLower() == "yes";
        return isPersishable;
    }
    
    private string GetName(string message)
    {   while(true)
        {   
            Console.Write(message);
            string name = Console.ReadLine();
            if(!String.IsNullOrWhiteSpace(name))
            {
                return name; 
            }    
            Console.WriteLine("Product name cannot be null or Empty");;
        }
    }


    private double GetPrice(string message)
    {   double price;
        while(true)
        {
            Console.Write($"{message}");
            var userInput = Console.ReadLine();
            if(double.TryParse(userInput,out price) && price > 0)
            {
                return price;
            }
            Console.WriteLine("Invalid Price !!");
        }       
    }

    private int GetQuantity(string message)
    {   int quantity;
        while(true)
        {
            Console.Write(message);
            var userInput = Console.ReadLine();
            if( int.TryParse(userInput, out quantity) && quantity > 0 )
            {
                return quantity;
            }
            Console.WriteLine("Invalid Quantity!!");
        } 
    }

    private DateOnly GetExpiryDate(string message)
    {
        DateOnly expiryDate;
        while(true)
        {
            Console.Write(message);
            var userInput =Console.ReadLine();
            if(DateOnly.TryParse(userInput,out expiryDate) && expiryDate > DateOnly.FromDateTime(DateTime.Now))
            {
                return expiryDate;
            }
            Console.WriteLine($"Invalid Date - Date cannot be in Past !!");
        }
        
    }
}
