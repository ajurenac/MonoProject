using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.ViewModels;
using Project.Service.Models;
using Project.Service.Services.Interfaces;

namespace Project.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleService vehicleService, IMapper mapper)
        {
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

            var mappedMakes = _mapper.Map<IEnumerable<VehicleModelViewModel>>(models).ToList();

            var viewModel = new VehicleModelViewModel
            {
                Models = new PaginatedList<VehicleModelViewModel>(mappedMakes, count, pageIndex, pageSize),
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

            var model = await _vehicleService.GetModelByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(model);

            return View(viewModel);
        }

        // GET: VehicleModel/Create
        public async Task<IActionResult> Create()
        {
            var makes = await _vehicleService.GetMakesAsync("Name", null, 1, int.MaxValue);
            var makeSelectListItems = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            var viewModel = new VehicleModelViewModel
            {
                Makes = makeSelectListItems
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


            if (ModelState.IsValid)
            {
                viewModel.MakeId = viewModel.DropDownMakeId;

                var model = _mapper.Map<VehicleModel>(viewModel);
                model.MakeId = viewModel.MakeId;

                await _vehicleService.AddModelAsync(model);
                return RedirectToAction(nameof(Index));
            }

            var makes = await _vehicleService.GetMakesAsync("Name", null, 1, int.MaxValue);

            viewModel.Makes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(viewModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _vehicleService.GetModelByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(model);

            var makes = await _vehicleService.GetMakesAsync("Name", null, 1, int.MaxValue);

            viewModel.Makes = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name,
                Selected = m.Id == model.MakeId
            }).ToList();

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

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                viewModel.MakeId = viewModel.DropDownMakeId;

                var model = _mapper.Map<VehicleModel>(viewModel);

                await _vehicleService.UpdateModelAsync(model);

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

            var model = await _vehicleService.GetModelByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleModelViewModel>(model);

            return View(viewModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteModelAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            return _vehicleService.GetModelByIdAsync(id) != null;
        }
    }
}
