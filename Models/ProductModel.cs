using DataScraper.Interfaces;

namespace DataScraper.Models;
public class ProductModel : IModel
{
    public string? productName;
    public string? price;
    public string? rating;
}