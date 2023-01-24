using AutoMapper;
using OMS.Common.Dtos.Categories;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
