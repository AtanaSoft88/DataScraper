using AutoMapper;
using DataScraper.Constants;
using DataScraper.Interfaces;
using DataScraper.Mappers;
using DataScraper.Models;

namespace DataScraper;
public class Program
{
    static void Main(string[] args)
    {
        string filePath = GlobalConstants.FilePath;

        IEnumerable<IProduct> products = PdfDataExtractor.ExtractData(filePath);
        if (!products.Any())
        {
            Console.WriteLine(GlobalConstants.ProductsNotAvailable);
            return;
        }
        IMapper mapper = Automapper.CreateInstance();
        var productModels = mapper.Map<IEnumerable<ProductModel>>(products);
        string jsonResult = JsonToStringConverter<IEnumerable<ProductModel>>.Convert(productModels);
        Console.WriteLine(jsonResult);
    }
}