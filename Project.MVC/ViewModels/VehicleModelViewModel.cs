using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.ViewModels;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMake Make { get; set; }   // single make entity

        public string CurrentSort { get; set; } 
        public string NameSort { get; set; } 
        public string AbrvSort { get; set; } 
        public string MakeSort { get; set; } 
        public string CurrentFilter { get; set; } 
        public string SearchString { get; set; } 

        public int DropDownMakeId { get; set; } // makeId from dropdown

        public List<SelectListItem> Makes { get; set; } // list of makes for dropdown
        public PaginatedList<VehicleModelViewModel> Models { get; set; } // list of vehicle models with pagination for use in Index view
    }
}
