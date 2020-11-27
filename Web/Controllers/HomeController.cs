using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assessment.Web.Models;
using Assessment.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Assessment.Data.Contexts;

namespace Assessment.Web.Controllers
{

    // www.localost.com/home/index
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AssessmentContext _context;

        public HomeController(ILogger<HomeController> logger, AssessmentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Requirements()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _context.Errors.Add(new Assessment.Models.Error {
                Timestamp = DateTime.UtcNow,
                UserName = User.Identity.Name,
                StackTrace = context.Error.StackTrace.ToString(),
                Source = context.Error.Source,
                Message = context.Error.Message,
                InnerException = context.Error.InnerException?.ToString(),
                HResult = context.Error.HResult,
            });
            _context.SaveChanges();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
