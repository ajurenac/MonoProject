using Project.MVC.ViewModels;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public PaginatedList<VehicleModel> Models { get; set; } // list of vehicle models with pagination for use in Index view
        public string CurrentSort { get; set; } 
        public string NameSort { get; set; } 
        public string AbrvSort { get; set; } 
        public string MakeSort { get; set; } 
        public string CurrentFilter { get; set; } 
        public string SearchString { get; set; } 
        public VehicleModel VehicleModel { get; set; } // a single vehicle model for use in Create, Details, Edit, and Delete views
        public List<VehicleMake> Makes { get; set; }  // list of makes for dropdown
        public int DropDownMakeId { get; set; } // makeId from dropdown
        public VehicleMake Make { get; set; } // single make entity
    }
}
