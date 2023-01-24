using AutoMapper;
using OMS.Common.Dtos.Products;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.CategoryName));
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<CreateProductDto, Product>();

        }
    }
}
