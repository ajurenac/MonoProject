using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Repositories.Interfaces;

namespace Project.Service.Repositories.Implementations
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleDbContext _context;

        public VehicleModelRepository(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(string sortOrder, string searchString, int pageIndex, int pageSize)
        {
            var query = _context.VehicleModels.Include(vm => vm.Make).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Make.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(m => m.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                case "make_desc":
                    query = query.OrderByDescending(m => m.Make.Name);
                    break;
                default:
                    query = query.OrderBy(m => m.Make.Name);
                    break;
            }

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VehicleModel> GetByIdAsync(int id)
        {
            return await _context.VehicleModels.Include(vm => vm.Make).FirstOrDefaultAsync(vm => vm.Id == id);
        }

        public async Task<int> GetCountAsync(string searchString)
        {
            var query = _context.VehicleModels.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Make.Name.Contains(searchString));
            }

            return await query.CountAsync();
        }

        public async Task AddAsync(VehicleModel model)
        {
            await _context.VehicleModels.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleModel model)
        {
            _context.VehicleModels.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.VehicleModels.FindAsync(id);
            if (model != null)
            {
                _context.VehicleModels.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}
