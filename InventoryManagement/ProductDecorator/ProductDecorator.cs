namespace InventoryManagement.ProductDecorator;

public abstract class ProductDecorator : IProduct
{
    protected readonly IProduct _product;
    public ProductDecorator(IProduct product){
        _product = product;
    }
    public virtual string Name{
        get => _product.Name;
        set => _product.Name = value;
    }
    public virtual double Price{
        get => _product.Price;
        set => _product.Price = value;
    }
    public virtual int Quantity{
        get => _product.Quantity;
        set => _product.Quantity = value;
    }
    public virtual string GetDetails() => _product.GetDetails();
}
