using AutoMapper;

namespace DataScraper.Mappers;
public class Automapper
{
    public static IMapper CreateInstance() 
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
        });
        IMapper mapper = new Mapper(mapperConfig);
        return mapper;
    } 
}
