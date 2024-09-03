using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Repositories.Interfaces;

namespace Project.Service.Repositories.Implementations
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly VehicleDbContext _context;

        public VehicleMakeRepository(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync(string sortOrder, string searchString, int pageIndex, int pageSize)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<VehicleMake> GetByIdAsync(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task<int> GetCountAsync(string searchString)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString));
            }

            return await query.CountAsync();
        }

        public async Task AddAsync(VehicleMake make)
        {
            await _context.VehicleMakes.AddAsync(make);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleMake make)
        {
            _context.VehicleMakes.Update(make);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var make = await _context.VehicleMakes.FindAsync(id);
            if (make != null)
            {
                _context.VehicleMakes.Remove(make);
                await _context.SaveChangesAsync();
            }
        }
    }
}
