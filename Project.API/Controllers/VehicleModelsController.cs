using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.API.DTOs;
using Project.Service.Models;
using Project.Service.Services.Interfaces;

namespace Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleModelsController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelsController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels([FromQuery] string sortOrder, [FromQuery] string searchString, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var models = await _vehicleService.GetModelsAsync(sortOrder, searchString, pageIndex, pageSize);
            var modelsDto = _mapper.Map<IEnumerable<VehicleModelDto>>(models);
            return Ok(modelsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null) return NotFound();

            var modelDto = _mapper.Map<VehicleModelDto>(model);
            return Ok(modelDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel([FromBody] VehicleModel modelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var model = _mapper.Map<VehicleModel>(modelDto);
            await _vehicleService.AddModelAsync(model);

            return CreatedAtAction(nameof(GetModelById), new { id = model.Id }, _mapper.Map<VehicleModelDto>(model));
        }

        public async Task<IActionResult> UpdateModel(int id, [FromBody] VehicleMakeDto modelDto)
        {
            if (id != modelDto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var model = _mapper.Map<VehicleModel>(modelDto);
            await _vehicleService.UpdateModelAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            await _vehicleService.DeleteModelAsync(id);
            return NoContent();
        }
    }
}
