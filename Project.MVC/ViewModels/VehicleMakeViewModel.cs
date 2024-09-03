using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public PaginatedList<VehicleMake> Makes { get; set; } // list of vehicle models with pagination for use in Index view
        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string AbrvSort { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public VehicleMake VehicleMake { get; set; } // a single vehicle make for use in Create, Details, Edit, and Delete views
    }
}
