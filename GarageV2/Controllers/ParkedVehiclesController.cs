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

namespace GarageV2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;

        public ParkedVehiclesController(GarageV2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ParkedVehicles
        /*
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }
        */
        // Get: Search Products
        public async Task<IActionResult> Index(string searchString)
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
            });
            

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

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNo,Type,Color,Brand,Model,NoWheels,TimeStamp")] ParkedVehicle parkedVehicle)
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

        // GET: ParkedVehicles/Edit/5
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

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();

            ReceiptParkingViewModel viewModel = _mapper.Map<ReceiptParkingViewModel>(parkedVehicle);
            viewModel.Checkout = DateTime.UtcNow.ToLocalTime();
            viewModel.TimeParked = viewModel.Checkout - viewModel.CheckIn;
            viewModel.Price = (decimal) viewModel.TimeParked.TotalMinutes;

            return View("Receipt", viewModel);
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
    }
}
