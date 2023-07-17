using DataScraper.Constants;
using DataScraper.Interfaces;

namespace DataScraper.Data;
public class Product : IProduct
{
    private string? productName = string.Empty;
    private decimal? price;
    private double? rating;    
    public Product(string? productName, decimal? price, double? rating)
    {
        ProductName = productName;
        Price = price;
        Rating = rating;
    }

    public string? ProductName
    {
        get => productName;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(GlobalConstants.InvalidProductName);
            }
            productName = value;
        }
    }

    public decimal? Price
    {
        get => price;
        private set
        {
            if (value < 0 || value is null)
            {
                throw new ArgumentException(GlobalConstants.InvalidProductPrice);
            }            
            price = value;
        }
    }

    public double? Rating
    {
        get => rating;
        private set
        {
            if (value < 1 || value > 10 || value is null)
            {
                throw new ArgumentException(GlobalConstants.InvalidProductRating);
            }
            rating = value;
        }
    }
}
