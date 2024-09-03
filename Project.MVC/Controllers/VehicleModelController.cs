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
    public class VehicleModelController : Controller
    {
        private readonly VehicleDbContext _context;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelController(VehicleDbContext context, IVehicleService vehicleService, IMapper mapper)
        {
            _context = context;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["MakeSort"] = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
            ViewData["AbrvSort"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewData["NameSort"] = sortOrder == "name" ? "name_desc" : "name";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var models = await _vehicleService.GetModelsAsync(sortOrder, searchString, pageIndex, pageSize);
            var count = await _vehicleService.GetModelCountAsync(searchString);

            var viewModel = new VehicleModelViewModel
            {
                Models = new PaginatedList<VehicleModel>(models.ToList(), count, pageIndex, pageSize),
                CurrentSort = sortOrder,
                NameSort = ViewData["NameSort"].ToString(),
                AbrvSort = ViewData["AbrvSort"].ToString(),
                MakeSort = ViewData["MakeSort"].ToString(),
                CurrentFilter = searchString,
                SearchString = searchString
            };

            return View(viewModel);
        }

        // GET: VehicleModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModels
                .Include(v => v.Make)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleModelViewModel
            {
                VehicleModel = vehicleModel
            };

            return View(viewModel);
        }

        // GET: VehicleModel/Create
        public IActionResult Create()
        {
            var viewModel = new VehicleModelViewModel
            {
                Makes = _context.VehicleMakes.ToList()
            };
            return View(viewModel);
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModelViewModel viewModel)
        {
            ModelState.Remove("Models");
            ModelState.Remove("CurrentSort");
            ModelState.Remove("NameSort");
            ModelState.Remove("AbrvSort");
            ModelState.Remove("MakeSort");
            ModelState.Remove("CurrentFilter");
            ModelState.Remove("SearchString");
            ModelState.Remove("VehicleModel.Make");
            ModelState.Remove("Makes");
            ModelState.Remove("Make");
            ModelState.Remove("VehicleModel");

            var vehicleModel = viewModel.VehicleModel;

            if (vehicleModel != null && ModelState.IsValid)
            {
                viewModel.VehicleModel.MakeId = viewModel.DropDownMakeId;
                _context.Add(viewModel.VehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["MakeId"] = new SelectList(_context.VehicleMakes, "Id", "Abrv", vehicleModel.MakeId);

            var viewModel = new VehicleModelViewModel
            {
                VehicleModel = vehicleModel
            };

            return View(viewModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModelViewModel viewModel)
        {
            ModelState.Remove("Models");
            ModelState.Remove("CurrentSort");
            ModelState.Remove("NameSort");
            ModelState.Remove("AbrvSort");
            ModelState.Remove("MakeSort");
            ModelState.Remove("CurrentFilter");
            ModelState.Remove("SearchString");
            ModelState.Remove("VehicleModel");
            ModelState.Remove("Makes");
            ModelState.Remove("Make");
            ModelState.Remove("VehicleModel.Make");

            var vehicleModel = viewModel.VehicleModel;

            if (vehicleModel == null || id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(vehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModels
                .Include(v => v.Make)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleModelViewModel
            {
                VehicleModel = vehicleModel
            };

            return View(viewModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await _context.VehicleModels.FindAsync(id);
            if (vehicleModel != null)
            {
                _context.VehicleModels.Remove(vehicleModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            return _context.VehicleModels.Any(e => e.Id == id);
        }
    }
}
