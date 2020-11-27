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

namespace Assessment.Web.Controllers
{

    // www.localost.com/home/index
    public class TestController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public TestController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            throw new NotImplementedException();
        }

    }
}
