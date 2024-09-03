using AutoMapper;
using Project.Service.Models;

namespace Project.Service.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleMake, VehicleMake>().ReverseMap();
            CreateMap<VehicleModel, VehicleModel>().ReverseMap();
        }
    }
}
