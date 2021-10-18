using AutoMapper;
using Tasevski.Services.ProductAPI.Models;

namespace Tasevski.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
                //config.CreateMap<Product, ProductDTO>();
            });
            return mappingConfig;
        }
    }
}