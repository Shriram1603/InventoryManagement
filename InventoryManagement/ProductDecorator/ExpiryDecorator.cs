namespace InventoryManagement.ProductDecorator;

public class ExpiryDecorator : ProductDecorator
{
    public DateOnly ExpiryDate {get; set; }

    public ExpiryDecorator(IProduct product, DateOnly expiryDate) : base(product)
    {
        ExpiryDate = expiryDate;
    }
    
    public bool IsExpired(){
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        return dateNow > ExpiryDate;
    }

    public override string GetDetails()
    {
        string baseDetails = base.GetDetails();
        string expiryStatus = IsExpired() ? "Expired" : "Not Expired";
        return $"{baseDetails}, Expiry Date : {ExpiryDate}, Status: {expiryStatus}";
    }
}