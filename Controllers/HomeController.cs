using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcFreelan.Data;
using MvcFreelan.GlobalVariables;
using MvcFreelan.Helpers;
using MvcFreelan.Helpers.Worklift;
using MvcFreelan.Models;
using MvcFreelan.Models.Forklift;
using MvcFreelan.Models.WorkliftMods;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace MvcFreelan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }

        public IActionResult Index()
        {
            string codedConn = configuration.GetConnectionString("conn");
            DbRenta db = new DbRenta(codedConn.DesEncriptar());
            ViewBag.resp = db.VerifConn();

            return View();
        }

        public IActionResult Mtrx()
        {
            return View();
        }

        public IActionResult Elevator()
        {
            MdlIndex mdlIndex = new MdlIndex(Info.minFloor, Info.maxFloor);
            return View(mdlIndex);
        }

        public IActionResult ReneCV()
        {
            MdlIndex mdlIndex = new MdlIndex(Info.minFloor, Info.maxFloor);
            return View(mdlIndex);
        }

        public IActionResult MotSeg()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
