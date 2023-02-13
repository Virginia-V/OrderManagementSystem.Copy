namespace OMS.Common.Dtos.Auth
{
    public class AuthResultDto
    {
        private AuthResultDto()
        {

        }
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Picture { get; set; }
        public string ContactInfo { get; set; }
        public bool EmailConfirmed { get; set; }
        public static AuthResultDto Failed(string error)
        {
            return new AuthResultDto
            {
                Error = error
            };
        }

        public static AuthResultDto Succeded(string email, UserInfoDto infoDto)
        {
            return new AuthResultDto
            {
                Succeeded = true,
                Email = email,
                Name = infoDto.Name,
                Id = infoDto.Id,
                Picture = infoDto.Picture,
                ContactInfo = infoDto.ContactInfo,
                EmailConfirmed = infoDto.EmailConfirmed
            };
        }
    }
}
