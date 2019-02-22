using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GarageV2.Models;

namespace GarageV2.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Action used for rendering the start page (home page)
        /// </summary>
        /// <returns>The view containing the home page</returns>
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Action method used for returning the view containing the custom Error 404 page.
        /// </summary>
        /// <returns>The Error404 View</returns>
        public IActionResult Error404()
        {
            return View();
        }
    }
}
