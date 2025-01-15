namespace InventoryManagement.ProductDecorator;

public abstract class ProductDecorator : IProduct
{
    protected readonly IProduct _Product;
    public ProductDecorator(IProduct product){
        _Product = product;
    }
    public virtual string Name{
        get => _Product.Name;
        set => _Product.Name = value;
    }
    public virtual double Price{
        get => _Product.Price;
        set => _Product.Price = value;
    }
    public virtual int Quantity{
        get => _Product.Quantity;
        set => _Product.Quantity = value;
    }
    public virtual string GetDetails() => _Product.GetDetails();
}
