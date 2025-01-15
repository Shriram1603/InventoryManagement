namespace InventoryManagement.Inventory;

using System.ComponentModel;
using InventoryManagement.ProductDecorator;

public class InventoryManager
{
    private IList<IProduct> _products = new List<IProduct>();

    public void Add(IProduct product)
    {   
        _products.Add(product);
    }

    public void RestockProduct(string productName,int quantity)
    {
        IProduct product = _products.FirstOrDefault( i => i.Name == productName);
        product.Quantity += quantity;
    }

    public void RemoveProduct(string name)
    {
        IProduct product = _products.FirstOrDefault( i => i.Name == name);
        _products.Remove(product);
    }

    public IList<string> DisplayProducts()
    {
        int serialNumber = 1;
        IList<string> products = new List<string>();
        foreach (var product in _products)
        {
            products.Add($"\t{serialNumber++}. {product.GetDetails()}");
        }
        return products;
    }

    public string SearchProduct(string name)
    {   
        IProduct product = _products.FirstOrDefault( i => i.Name == name);
        if(product != null)
        {
            return product.GetDetails();
        }
        return $"Product {name} is not present in the inventory !!";
    }

    public void UpdateProduct(string oldName,string name,double price,int quantity)
    {
        IProduct product = _products.FirstOrDefault( i => i.Name == oldName);
        product.Name = name;
        product.Price = price;
        product.Quantity = quantity;
    }
    public void UpdateProduct(string oldName,string name,double price,int quantity,DateOnly expiryDate)
    {
        ExpiryDecorator product = (ExpiryDecorator)_products.FirstOrDefault( i => i.Name == oldName);
        product.Name = name;
        product.Price = price;
        product.Quantity = quantity;
        product.ExpiryDate = expiryDate;
    }

    public IList<string> SortProducts()
    {
        IList<string> products = new List<string>();
        var ascending = _products.OrderBy(i => i.Name).ToList();
        var decending = _products.OrderByDescending(i => i.Name).ToList();
        products.Add("\tAscending Order [name] :");
        for(int i =0 ; i < _products.Count; i++)
        {
            products.Add($"\t\t{i+1}. {ascending[i].GetDetails()}");
        }
        products.Add("\n\tDecending Order[name] :");
        for(int i =0 ; i < _products.Count; i++)
        {
            products.Add($"\t\t{i+1}. {decending[i].GetDetails()}");
        }
        return products;

    }

    public bool IsProductPresent(string productName)
    {
        IProduct item = _products.FirstOrDefault(i => i.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        return item != null;
    }

    public bool IsPerishable(string name)
    {
        IProduct item = _products.FirstOrDefault( i => i.Name == name);
        return item is ExpiryDecorator;
    }
}