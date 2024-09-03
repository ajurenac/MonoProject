using Project.Service.Models;

namespace Project.Service.Repositories.Interfaces
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(string sortOrder, string searchString, int pageIndex, int pageSize);
        Task<VehicleModel> GetByIdAsync(int id);
        Task<int> GetCountAsync(string searchString);
        Task AddAsync(VehicleModel model);
        Task UpdateAsync(VehicleModel model);
        Task DeleteAsync(int id);
    }
}
