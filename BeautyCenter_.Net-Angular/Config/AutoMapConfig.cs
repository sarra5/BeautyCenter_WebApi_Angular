using AutoMapper;
using BeautyCenter_.Net_Angular.DTO;
//using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.Models;

namespace BeautyCenter_.Net_Angular.Config
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig() 
        {
            CreateMap<Userr, User>();
            CreateMap<User, Userr>();
            CreateMap<PackageService, serviceD>(); // Map PackageService to ServiceD
            CreateMap<Package, PackageD>(); // Map Package to PackageD
            CreateMap<PackageUserDTO, PackageUser>();
            CreateMap<PackageUser, PackageUserDTO>();


            CreateMap<serviceD,ServiceResponse>();
            CreateMap<ServiceResponse,serviceD>();

        }
    }
}
