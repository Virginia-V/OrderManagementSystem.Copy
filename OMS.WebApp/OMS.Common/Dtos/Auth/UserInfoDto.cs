using OMS.Domain.Interfaces;

namespace OMS.Common.Dtos.Auth
{
    public class UserInfoDto : IProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ContactInfo { get; set; }
        //public bool EmailConfirmed { get; set; }
        public bool IsSocialAccount { get; set; }
    }
}
