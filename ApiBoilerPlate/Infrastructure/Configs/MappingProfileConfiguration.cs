using AutoMapper;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO;
using ApiBoilerPlate.DTO.Response;
using ApiBoilerPlate.DTO.Request;

namespace ApiBoilerPlate.Infrastructure.Configs
{
    public class MappingProfileConfiguration: Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Personnel, AddPersonnelRequest>().ReverseMap();
            CreateMap<Personnel, PersonnelQueryResponse>().ReverseMap();
        }
    }
}
