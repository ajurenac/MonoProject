using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC.ViewModels;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Services.Interfaces;

namespace Project.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleMakeController : Controller
    {
        private readonly VehicleDbContext _context;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakeController(VehicleDbContext context, IVehicleService vehicleService, IMapper mapper)
        {
            _context = context;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSort"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var makes = await _vehicleService.GetMakesAsync(sortOrder, searchString, pageIndex, pageSize);
            var count = await _vehicleService.GetMakeCountAsync(searchString);

            var viewModel = new VehicleMakeViewModel
            {
                Makes = new PaginatedList<VehicleMake>(makes.ToList(), count, pageIndex, pageSize),
                CurrentSort = sortOrder,
                NameSort = ViewData["NameSort"].ToString(),
                AbrvSort = ViewData["AbrvSort"].ToString(),
                CurrentFilter = searchString,
                SearchString = searchString
            };

            return View(viewModel);
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleMakeViewModel
            {
                VehicleMake = vehicleMake
            };

            return View(viewModel);
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMakeViewModel viewModel)
        {
            ModelState.Remove("Makes");
            ModelState.Remove("CurrentSort");
            ModelState.Remove("NameSort");
            ModelState.Remove("AbrvSort");
            ModelState.Remove("CurrentFilter");
            ModelState.Remove("SearchString");

            var vehicleMake = viewModel.VehicleMake;

            if (vehicleMake != null && ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleMakeViewModel
            {
                VehicleMake = vehicleMake
            };

            return View(viewModel);
        }

        // POST: VehicleMake/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleMakeViewModel viewModel)
        {
            ModelState.Remove("Makes");
            ModelState.Remove("CurrentSort");
            ModelState.Remove("NameSort");
            ModelState.Remove("AbrvSort");
            ModelState.Remove("CurrentFilter");
            ModelState.Remove("SearchString");

            var vehicleMake = viewModel.VehicleMake;
            if (vehicleMake == null || id != vehicleMake.Id)
            {
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                _context.Update(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }

            return View(viewModel);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleMakeViewModel
            {
                VehicleMake = vehicleMake
            };

            return View(viewModel);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake != null)
            {
                _context.VehicleMakes.Remove(vehicleMake);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMakes.Any(e => e.Id == id);
        }
    }
}
