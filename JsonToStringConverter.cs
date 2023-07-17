using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DataScraper.Interfaces;

namespace DataScraper;
public class JsonToStringConverter<T> 
    where T : IEnumerable<IModel>
{
    public static string Convert(T items)
    
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,            
            
        };        
        string jsonObjects = JsonConvert.SerializeObject(items, settings);
        return jsonObjects;
    }
}

