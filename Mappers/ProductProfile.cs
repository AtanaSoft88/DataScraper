using AutoMapper;
using DataScraper.Data;
using DataScraper.Models;

namespace DataScraper.Mappers;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}
