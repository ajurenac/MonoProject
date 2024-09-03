using AutoMapper;
using Project.Service.Models;
using Project.Service.Repositories.Interfaces;
using Project.Service.Services.Interfaces;

namespace Project.Service.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleMakeRepository _makeRepository;
        private readonly IVehicleModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleMakeRepository makeRepository, IVehicleModelRepository modelRepository, IMapper mapper)
        {
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleMake>> GetMakesAsync(string sortOrder, string searchString, int pageIndex, int pageSize)
        {
            return await _makeRepository.GetAllAsync(sortOrder, searchString, pageIndex, pageSize);
        }

        public async Task<VehicleMake> GetMakeByIdAsync(int id)
        {
            return await _makeRepository.GetByIdAsync(id);
        }

        public async Task<int> GetMakeCountAsync(string searchString)
        {
            return await _makeRepository.GetCountAsync(searchString);
        }

        public async Task AddMakeAsync(VehicleMake make)
        {
            await _makeRepository.AddAsync(make);
        }

        public async Task UpdateMakeAsync(VehicleMake make)
        {
            await _makeRepository.UpdateAsync(make);
        }

        public async Task DeleteMakeAsync(int id)
        {
            await _makeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehicleModel>> GetModelsAsync(string sortOrder, string searchString, int pageIndex, int pageSize)
        {
            return await _modelRepository.GetAllAsync(sortOrder, searchString, pageIndex, pageSize);
        }

        public async Task<VehicleModel> GetModelByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<int> GetModelCountAsync(string searchString)
        {
            return await _modelRepository.GetCountAsync(searchString);
        }

        public async Task AddModelAsync(VehicleModel model)
        {
            await _modelRepository.AddAsync(model);
        }

        public async Task UpdateModelAsync(VehicleModel model)
        {
            await _modelRepository.UpdateAsync(model);
        }

        public async Task DeleteModelAsync(int id)
        {
            await _modelRepository.DeleteAsync(id);
        }
    }
}
