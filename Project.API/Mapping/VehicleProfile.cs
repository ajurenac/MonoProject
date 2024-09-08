using AutoMapper;
using Project.Service.Models;
using Project.API.DTOs;

namespace Project.API.Mapping
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelDto>().ReverseMap();
        }
    }
}
