namespace InventoryManagement.ProductDecorator;

public class Product : IProduct 
{   
    Guid Id = Guid.NewGuid();
    public string Name {get; set; }
    public double Price {get; set;}
    public int Quantity {get; set; }

    public Product(string name, double price, int quantity)
    {
        Name =name;
        Price = price;
        Quantity = quantity;
    }
    public string GetDetails() => $"ID: {Id}, Product : {Name}, Price: {Price}, Quantity: {Quantity}";
}
