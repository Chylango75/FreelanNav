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
            string decodedConn = codedConn.DesEncriptar();
            DbRenta db = new DbRenta(decodedConn);
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


        [HttpPost]
        public JsonResult Go(Worklifter worklift)
        {
            // Clean Info from font in case needed
            worklift.StrNextFloors = worklift.StrNextFloors == null ? "" : worklift.StrNextFloors;
            worklift.StrReqsElevator = worklift.StrReqsElevator == null ? "" : worklift.StrReqsElevator;
            worklift.StrReqsBuilding = worklift.StrReqsBuilding == null ? "" : worklift.StrReqsBuilding;

            //   Get requests from elevator
            ConvertReqElevatorToPisos strElevatorToPisos = new ConvertReqElevatorToPisos(worklift.StrReqsElevator);
            IEnumerable<Piso> reqsElevator = strElevatorToPisos.StrToPisos();

            //   Get requests from building floors
            ConvertReqBuildingToPisos strBuildingToPisos = new ConvertReqBuildingToPisos(worklift.StrReqsBuilding);
            IEnumerable<Piso> reqsBuilding = strBuildingToPisos.StrToPisos();

            //   Get all previous petitions
            ConvertReqPrevsToPisos reqPrevsToPisos = new ConvertReqPrevsToPisos(worklift.StrNextFloors);
            IEnumerable<Piso> reqsPrevs = reqPrevsToPisos.StrToPisos();

            //  Join all pisos and accommodate
            var allPisosReqs = reqsElevator.Concat(reqsBuilding).Concat(reqsPrevs);
            AccommodatePisos setPisos = new AccommodatePisos(allPisosReqs);
            worklift.FloorsToVisit = setPisos.GetOrdered();

            // If there are no requests, it goes back to client with no actions
            if (worklift.FloorsToVisit.Count() == 0)
            {
                worklift.StrNextFloors = "";
                worklift.OpenDoors = false;
                return Json(worklift);
            }

            //   If there are pending requests, eleminate current floor from "Floors to visit" if posible 
            ProcessCurrentPiso procesPiso = new ProcessCurrentPiso(worklift);
            worklift = procesPiso.SetPiso();

            //  Client only works with strings, so lists are converted into string to be sent back
            ConvertPisosToStr convertPisosToStr = new ConvertPisosToStr(worklift.FloorsToVisit);
            worklift.StrNextFloors = convertPisosToStr.PisosToString();

            // Clean vars to send back to client avoiding transit extra
            worklift.StrReqsElevator = "";
            worklift.StrReqsBuilding = "";
            worklift.FloorsToVisit = new List<Piso>();

            return Json(worklift);
        }

        [HttpPost]
        public PartialViewResult DrawBuilding(int piso, bool OpenDoors)
        {
            Ubica ubica = new Ubica(piso, OpenDoors);
            return PartialView("_DrawBuilding", ubica);
        }
    }
}
