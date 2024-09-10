using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service.Models;

namespace Project.MVC.Mapping
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}
