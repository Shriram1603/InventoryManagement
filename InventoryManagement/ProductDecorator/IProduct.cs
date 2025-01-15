namespace InventoryManagement.ProductDecorator;

public interface IProduct 
{   
    string Name {get;set;}
    double Price {get;set;}
    int Quantity {get; set; }
    // DateOnly ExpiryDate{get; set;}

    string GetDetails();
}
