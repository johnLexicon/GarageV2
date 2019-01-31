using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageV2.Controllers
{
    public class DebugController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Controller for checking Environment and paths info
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public DebugController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Environment()
        {
            return Ok(_hostingEnvironment.EnvironmentName);
        }

        public IActionResult ContentRootPath()
        {
            return Ok(_hostingEnvironment.ContentRootPath);
        }

        public IActionResult WebRootPath()
        {
            return Ok(_hostingEnvironment.WebRootPath);
        }
    }
}
