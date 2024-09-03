using Project.Service.Models;

namespace Project.Service.Repositories.Interfaces
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetAllAsync(string sortOrder, string searchString, int pageIndex, int pageSize);
        Task<VehicleMake> GetByIdAsync(int id);
        Task<int> GetCountAsync(string searchString);
        Task AddAsync(VehicleMake make);
        Task UpdateAsync(VehicleMake make);
        Task DeleteAsync(int id);
    }
}
