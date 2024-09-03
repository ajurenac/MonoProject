using Project.Service.Models;

namespace Project.Service.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetMakesAsync(string sortOrder, string searchString, int pageIndex, int pageSize);
        Task<VehicleMake> GetMakeByIdAsync(int id);
        Task<int> GetMakeCountAsync(string searchString);
        Task AddMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(int id);

        Task<IEnumerable<VehicleModel>> GetModelsAsync(string sortOrder, string searchString, int pageIndex, int pageSize);
        Task<VehicleModel> GetModelByIdAsync(int id);
        Task<int> GetModelCountAsync(string searchString);
        Task AddModelAsync(VehicleModel model);
        Task UpdateModelAsync(VehicleModel model);
        Task DeleteModelAsync(int id);
    }
}
