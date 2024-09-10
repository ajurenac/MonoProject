using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string AbrvSort { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        
        public PaginatedList<VehicleMakeViewModel> Makes { get; set; } // list of vehicle models with pagination for use in Index view

    }
}
