using AutoMapper;
using WebApiConfigurationn.Entities.Auth;
using WebApiConfigurationn.Entities.DTOs.Auth;

namespace WebApiConfigurationn.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterDTO, AppUser<Guid>>().ReverseMap();
        }
    }
}
