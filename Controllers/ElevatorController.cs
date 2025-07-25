using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFreelan.GlobalVariables;
using MvcFreelan.Models.Forklift;

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

    }
}
