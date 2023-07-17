namespace DataScraper.Interfaces;
public interface IProduct 
{
    string? ProductName { get; } 
    decimal? Price { get; } 
    double? Rating { get; }
}