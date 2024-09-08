using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.API.DTOs;
using Project.Service.Models;
using Project.Service.Services.Interfaces;

namespace Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakesController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMakes([FromQuery] string sortOrder, [FromQuery] string searchString, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var makes = await _vehicleService.GetMakesAsync(sortOrder, searchString, pageIndex, pageSize);
            var makesDto = _mapper.Map<IEnumerable<VehicleMakeDto>>(makes);
            return Ok(makesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMakeById(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null) return NotFound();

            var makeDto = _mapper.Map<VehicleMakeDto>(make);
            return Ok(makeDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddMake([FromBody] VehicleMakeDto makeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var make = _mapper.Map<VehicleMake>(makeDto);
            await _vehicleService.AddMakeAsync(make);
            return CreatedAtAction(nameof(GetMakeById), new { id = make.Id }, _mapper.Map<VehicleMakeDto>(make));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] VehicleMakeDto makeDto)
        {
            if (id != makeDto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var make = _mapper.Map<VehicleMake>(makeDto);
            await _vehicleService.UpdateMakeAsync(make);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            await _vehicleService.DeleteMakeAsync(id);
            return NoContent();
        }
    }
}
