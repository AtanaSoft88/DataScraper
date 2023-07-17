namespace DataScraper.Constants;
public static class GlobalConstants
{
    public const string FilePath = @"./Pdf/sampleDocument.pdf";

    //Product errors
    public const string ProductsNotAvailable = "There was no products found.";
    public const string InvalidProductName = "Product name cannot be null or empty.";
    public const string InvalidProductPrice = "Price cannot be null or negative.";
    public const string InvalidProductRating = "Invalid Rating input value.";
}
