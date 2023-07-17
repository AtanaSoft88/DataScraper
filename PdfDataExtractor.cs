using HtmlAgilityPack;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Globalization;
using System.Net;
using System.Text;
using DataScraper.Data;

namespace DataScraper;
public class PdfDataExtractor   
{
    public static IEnumerable<Product> ExtractData(string path)
    {
        using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(path)))
        {
            int numberOfPages = pdfDocument.GetNumberOfPages();
            StringBuilder stringBuilder = new StringBuilder();
            for (int page = 1; page <= numberOfPages; page++)
            {
                var strategy = new LocationTextExtractionStrategy();
                PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
                parser.ProcessPageContent(pdfDocument.GetPage(page));

                stringBuilder.Append(strategy.GetResultantText());

            }
            string resultTextFromPdf = stringBuilder.ToString().TrimEnd();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(resultTextFromPdf);

            var htmlNodeElements = document
                 .DocumentNode
                 .SelectNodes("div")
                 .Where(node => node.Attributes["class"].Value != null
                    && node.Attributes["class"].Value == "item")
                 .ToList();
            IList<Product> products = new List<Product>();
            foreach (var nodeElement in htmlNodeElements)
            {
                var format = new NumberFormatInfo();
                format.NumberDecimalSeparator = ".";
                format.NumberGroupSeparator = ",";

                string? productName = GetProductName(nodeElement);
                double? rating = GetProductRating(nodeElement, format);
                decimal? price = GetProductPrice(nodeElement, format);

                var product = new Product(productName,price,rating);                    
                products.Add(product);
            }
            return products;
        }
    }

    private static decimal? GetProductPrice(HtmlNode nodeElement, NumberFormatInfo format)
    {
        try
        {

            HtmlNode? priceSpanElement = nodeElement
                    .Descendants("span")
                    .Where(a => a.Attributes["class"].Value != null &&
                                a.Attributes["class"].Value == "price-display formatted")
                    .FirstOrDefault();
            string? priceAsString = priceSpanElement?
                               .FirstChild
                               .InnerText
                               .ToString()
                               .Trim('$');
            bool isParsed = decimal.TryParse(priceAsString, NumberStyles.Any, format, out decimal price);
            if (!isParsed)
            {
                return null;
            }
            return price;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    private static double? GetProductRating(HtmlNode nodeElement, NumberFormatInfo format)
    {
        try
        {
            var ratingAsString = nodeElement
                    .GetAttributeValue("rating", "")
                    .ToString();
            bool isParsed = double.TryParse(ratingAsString, NumberStyles.Any, format, out double rating);
            if (!isParsed)
            {
                return null;
            }            
            return NormalizeToFive(rating);
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    private static string? GetProductName(HtmlNode nodeElement)
    {
        try
        {
            string? productName = nodeElement
                    .Descendants("img")
                    .Select(atributes => atributes.Attributes["alt"].Value)
                    .FirstOrDefault();
            if (productName is null)
            {
                return null;
            }
            string decodedProducName = DecodeText(productName);
            return decodedProducName;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    private static double NormalizeToFive(double rating)
    {       
        if (rating >= 0 && rating <= 10)
        {
            if (rating > 5)
            {
                rating = rating * 5 / 10;
            }
        }
        return rating;
    }
    private static string DecodeText(string text)
    {
        string decodedText = WebUtility.HtmlDecode(text);
        return decodedText;
    }
}