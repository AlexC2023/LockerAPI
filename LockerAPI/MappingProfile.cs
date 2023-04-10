using AutoMapper;
using LockerAPI.DTOs;
using LockerAPI.DTOs.CreateUpdateObjects;

namespace LockerAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CreateUpdateCompany>().ReverseMap();
        }
    }
}
