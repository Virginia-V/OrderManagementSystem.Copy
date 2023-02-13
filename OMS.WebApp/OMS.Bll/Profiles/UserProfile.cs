using AutoMapper;
using OMS.Common.Dtos.Auth;
using OMS.Domain;

namespace OMS.Bll.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfoDto>();
        }
    }
}
