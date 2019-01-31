using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageV2.Models;
using GarageV2.ViewModels;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using GarageV2.Services;

namespace GarageV2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;
        private readonly ParkedVehicleGenerator _vehicleGenerator;
        private readonly GarageSettings _garageSettings;

        public ParkedVehiclesController(GarageV2Context context, IMapper mapper, ParkedVehicleGenerator vehicleGenerator, GarageSettings garageSettings)
        {
            _context = context;
            _mapper = mapper;
            _vehicleGenerator = vehicleGenerator;
            _garageSettings = garageSettings;
        }

        // Get: Search Products
        public IActionResult Index(string searchString)
        {
            var model = from m in _context.ParkedVehicle
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = _context.ParkedVehicle.Where(pv => pv.RegNo.Contains(searchString));
            }

            var parkedVehicles = model.ToList();

            IEnumerable<ParkedCarViewModel> ParkedCarViewModel = parkedVehicles.Select(p =>
            {
                var viewModel = _mapper.Map<ParkedCarViewModel>(p);
                viewModel.TimeParked = (DateTime.UtcNow.ToLocalTime() - p.CheckIn);
                return viewModel;
            }).OrderByDescending(vm => vm.TimeParked);


            return View(ParkedCarViewModel);            
        }


        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            DetailVehicleViewModel viewModel = _mapper.Map<DetailVehicleViewModel>(parkedVehicle);

            return View(viewModel);
        }


        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var viewModel = new AddOrEditViewModel()
                {
                    AlreadyParked = false
                };
                return View(viewModel);
            }

            else
            {
                var parkedVehicle = _context.ParkedVehicle.Find(id);
                var viewModel = _mapper.Map<AddOrEditViewModel>(parkedVehicle);
                viewModel.AlreadyParked = true;

                return View(viewModel);
            }
                

                
        }


        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNo,ParkedVehicleType,Color,Brand,Model,NoWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.CheckIn = DateTime.UtcNow.ToLocalTime();
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }
        */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,RegNo,ParkedVehicleType,Color,Brand,Model,NoWheels,CheckIn,AlreadyParked")] AddOrEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!viewModel.AlreadyParked)
                {
                    viewModel.CheckIn = DateTime.UtcNow.ToLocalTime();
                    var parkedVehicle = _mapper.Map<ParkedVehicle>(viewModel);
                    _context.Add(parkedVehicle);
                }                    
                else
                {
                    var parkedVehicle = _mapper.Map<ParkedVehicle>(viewModel);
                    _context.Update(parkedVehicle);
                }
                    
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: ParkedVehicles/Edit/5
        /*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }
        */

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,ParkedVehicleType,Color,Brand,Model,NoWheels,CheckIn")]ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }
        */

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();

            ReceiptParkingViewModel viewModel = _mapper.Map<ReceiptParkingViewModel>(parkedVehicle);
            viewModel.Checkout = DateTime.UtcNow.ToLocalTime();
            viewModel.Price = _garageSettings.PricePerMinute;

            return View("Receipt", viewModel);
        }

        public IActionResult CheckIfRegNoExists(string regNo, int id)
        {

            var foundVehicle = _context.ParkedVehicle.FirstOrDefault(p => p.RegNo.Equals(regNo.ToUpper()));

            if(foundVehicle != null && foundVehicle.Id != id)
            {
                return Json($"Reg-nummer {regNo} finns redan.");
            }
            return Json(true);
        }

        [Route("/generate/{noParkedVehicles}")]
        public IActionResult GenerateParkedVehicles(int noParkedVehicles)
        {

            for (int i = 0; i < noParkedVehicles; i++)
            {
                var generatedVehicle = _vehicleGenerator.GenerateVehicle();
                if(_context.ParkedVehicle.FirstOrDefault(p => p.RegNo.Equals(generatedVehicle.RegNo)) is null)
                {
                    _context.Add(generatedVehicle);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        /*
        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
        */
    }
}
