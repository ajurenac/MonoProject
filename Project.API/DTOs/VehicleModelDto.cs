using Project.Service.Models;

namespace Project.API.DTOs
{
    public class VehicleModelDto
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMake Make { get; set; }
    }
}
