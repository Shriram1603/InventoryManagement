namespace InventoryManagement.ProductDecorator;

public interface IProduct 
{   
    string Name {get;set;}
    double Price {get;set;}
    int Quantity {get; set; }
    string GetDetails();
}
