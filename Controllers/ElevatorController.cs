using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFreelan.GlobalVariables;
using MvcFreelan.Helpers.Worklift;
using MvcFreelan.Models.Forklift;
using MvcFreelan.Models.WorkliftMods;

namespace MvcFreelan.Controllers
{
    public class ElevatorController : Controller
    {
        // GET: ElevatorController1
        public IActionResult Index()
        {
            MdlIndex mdlIndex = new MdlIndex(Info.minFloor, Info.maxFloor);
            return View(mdlIndex);
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
