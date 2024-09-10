using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service.Models;
using Project.Service.Services.Interfaces;

namespace Project.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper)
        {
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

            var mappedMakes = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(makes).ToList();

            var viewModel = new VehicleMakeViewModel
            {
                Makes = new PaginatedList<VehicleMakeViewModel>(mappedMakes, count, pageIndex, pageSize),
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

            var make = await _vehicleService.GetMakeByIdAsync(id.Value);
            if (make == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleMakeViewModel>(make);

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

            if (ModelState.IsValid)
            {
                var make = _mapper.Map<VehicleMake>(viewModel);

                await _vehicleService.AddMakeAsync(make);

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

            var make = await _vehicleService.GetMakeByIdAsync(id.Value);
            if (make == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleMakeViewModel>(make);

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

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var make = _mapper.Map<VehicleMake>(viewModel);

                await _vehicleService.UpdateMakeAsync(make);

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

            var make = await _vehicleService.GetMakeByIdAsync(id.Value);
            if (make == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleMakeViewModel>(make);

            return View(viewModel);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return _vehicleService.GetMakeByIdAsync(id) != null;
        }
    }
}
