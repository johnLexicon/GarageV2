using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageV2.Models;
using GarageV2.ViewModels;
using AutoMapper;
using GarageV2.Services;
using AutoMapper.QueryableExtensions;

namespace GarageV2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly GarageV2Context _context;
        private readonly IMapper _mapper;
        private readonly ParkedVehicleGenerator _vehicleGenerator;
        private readonly GarageSettings _garageSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The dbContext for parked vehicles</param>
        /// <param name="mapper">For automapping</param>
        /// <param name="vehicleGenerator">Helper for generating vehicles</param>
        /// <param name="garageSettings">For accessing garage settings from Config file</param>
        public ParkedVehiclesController(GarageV2Context context, IMapper mapper, ParkedVehicleGenerator vehicleGenerator, GarageSettings garageSettings)
        {
            _context = context;
            _mapper = mapper;
            _vehicleGenerator = vehicleGenerator;
            _garageSettings = garageSettings;
        }

        // Get: Search Products

        /// <summary>
        /// List of vehicles in the parking lot
        /// </summary>
        /// <param name="searchString">a string for filtering the vehicles by the reg number.</param>
        /// <returns>The Index View</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            //Query for retrieving all vehicles.
            var parkedVehicles = from pv in _context.ParkedVehicle select pv;

            // --- Filter ---

            if (!String.IsNullOrEmpty(searchString))
            {
                //Query for retrieving vehicles that contain the searchstring in the reg number.
                parkedVehicles = _context.ParkedVehicle

                .Where(
                    pv => pv.Member.Email.ToUpper().Contains(searchString.ToUpper()) ||
                    pv.RegNo.ToUpper().Contains(searchString.ToUpper()) ||
                    pv.VehicleType.Name.ToLower().Contains(searchString.ToLower()) ||
                    pv.Brand.ToLower().Contains(searchString.ToLower()) ||
                    pv.Model.ToLower().Contains(searchString.ToLower()) ||
                    pv.Color.ToLower().Contains(searchString.ToLower())

                );

            }


            // --- Create ParkedCarViewModel ---

            var parkedCarViewModels = await parkedVehicles
                .ProjectTo<ParkedCarViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(vm => vm.TimeParked)
                .ToListAsync();

            return View(parkedCarViewModels);
        }


        // GET: ParkedVehicles/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The id of the parked vehicle</param>
        /// <returns>The Details view</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(pv => pv.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            //_context.Entry(parkedVehicle).Reference(pv => pv.VehicleType).Load();
            DetailVehicleViewModel viewModel = _mapper.Map<DetailVehicleViewModel>(parkedVehicle);

            return View(viewModel);
        }

        /// <summary>
        /// Checks if it is an add or edit command. Redirects to overridden action method.
        /// </summary>
        /// <param name="id">The id of the vehicle</param>
        /// <returns></returns>
        public IActionResult AddOrEdit(int id = 0)
        {
            var parkedVehicleTypes = _context.VehicleType.ToList();
            var members = _context.Member.ToList();

            //Create
            if (id == 0)
            {
                var viewModel = new AddOrEditViewModel()
                {
                    AlreadyParked = false,
                    ParkedVehicleTypes = parkedVehicleTypes,
                    Members = members
                };
                return View(viewModel);
            }
            // - Edit -
            else
            {
                var parkedVehicle = _context.ParkedVehicle.Find(id);
                var viewModel = _mapper.Map<AddOrEditViewModel>(parkedVehicle);
                viewModel.ParkedVehicleTypes = parkedVehicleTypes;
                viewModel.AlreadyParked = true;
                viewModel.Members = members;
                return View(viewModel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel">The Viewmodel for the AddOrEdit view</param>
        /// <returns>The AddOrEdit view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(AddOrEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = _context.Member.Find(viewModel.MemberId);
                var vehicleType = _context.VehicleType.Find(viewModel.VehicleTypeId);

                if (!viewModel.AlreadyParked)
                {
                    viewModel.CheckIn = DateTime.UtcNow.ToLocalTime();
                    var parkedVehicle = _mapper.Map<ParkedVehicle>(viewModel);
                    parkedVehicle.Member = member;
                    parkedVehicle.VehicleType = vehicleType;
                    _context.Add(parkedVehicle);
                }
                else
                {
                    var parkedVehicle = _mapper.Map<ParkedVehicle>(viewModel);
                    parkedVehicle.VehicleType = vehicleType;
                    parkedVehicle.Member = member;
                    _context.Update(parkedVehicle);

                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        /// <summary>
        /// Action that calculates the the total and shows the receipt information 
        /// </summary>
        /// <param name="id">The id of the checked out vehicle</param>
        /// <returns>The Receipt view</returns>
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle
                .Include(pv => pv.VehicleType)
                .Include(pv => pv.Member)
                .FirstOrDefaultAsync(pv => pv.Id == id);

            //Redirects to not found (Error404 page) if parked vehicle is null.
            if (parkedVehicle is null)
            {
                return NotFound();
            }

            //_context.Entry(parkedVehicle).Reference(pv => pv.Member).Load();
            //_context.Entry(parkedVehicle).Reference(pv => pv.VehicleType).Load();

            ReceiptParkingViewModel viewModel = _mapper.Map<ReceiptParkingViewModel>(parkedVehicle);

            _context.ParkedVehicle.Remove(parkedVehicle);

            await _context.SaveChangesAsync();
            //_context.Entry(p).Reference(v => v.Member).Load();

            viewModel.Checkout = DateTime.UtcNow.ToLocalTime();
            viewModel.Price = _garageSettings.PricePerMinute;

            return View("Receipt", viewModel);
        }

        /// <summary>
        /// Helper Action used for Remote validation for the uniqueness of Reg number in the view class ViewModels/AddOrEditViewModel.cs
        /// </summary>
        /// <param name="regNo">The reg number to check</param>
        /// <param name="id">The id of the parked vehicle (id = 0 if the vehicle is parking)</param>
        /// <returns></returns>
        public IActionResult CheckIfRegNoExists(string regNo, int id)
        {
            if (regNo is null)
            {
                return NotFound();
            }

            var foundVehicle = _context.ParkedVehicle.FirstOrDefault(p => p.RegNo.Equals(regNo.ToUpper()));

            if (foundVehicle != null && foundVehicle.Id != id)
            {
                return Json($"Reg-nummer {regNo} finns redan.");
            }
            return Json(true);
        }

        /// <summary>
        /// Helper Action method for generating data (seed database) of parked vehicles. 
        /// Ex Use from URL localhost/Generate/20  => Generate 20 records in database
        /// </summary>
        /// <param name="noParkedVehicles">The number of parked vehicles to generate.</param>
        /// <returns>The view with the list of the parked vehicles.</returns>
        [Route("ParkedVehicles/Generate/{noParkedVehicles}")]
        public IActionResult GenerateParkedVehicles(int noParkedVehicles = 5)
        {
            Random rnd = new Random();
            var existingRegNumbers = _context.ParkedVehicle.Select(pv => pv.RegNo).ToList();
            var existingMembers = _context.Member.ToList();
            var existingVehicleTypes = _context.VehicleType.ToList();


            if (!existingMembers.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            for (int i = 0; i < noParkedVehicles; i++)
            {
                var generatedVehicle = _vehicleGenerator.GenerateVehicle();

                if (existingRegNumbers.IndexOf(generatedVehicle.RegNo) == -1)
                {
                    existingRegNumbers.Add(generatedVehicle.RegNo);
                    generatedVehicle.Member = existingMembers.ElementAt(rnd.Next(existingMembers.Count() - 1));
                    if (existingVehicleTypes.Any())
                    {
                        generatedVehicle.VehicleType = existingVehicleTypes.ElementAt(rnd.Next(existingVehicleTypes.Count() - 1));
                    }

                    _context.Add(generatedVehicle);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
