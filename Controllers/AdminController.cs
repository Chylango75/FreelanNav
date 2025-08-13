using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MvcFreelan.Controllers
{
    public class AdminController : Controller
    {
        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var path = "/Pics/000Error.jpg";

            if (statusCode > 0)
            {
                if (statusCode >= 400 && statusCode <= 500)
                    path = "/Pics/400NotFound.jpg";
                if (statusCode >= 500)
                    path = "/Pics/500InternalError.jpg";
            }

            ViewBag.path = path;

            return View();
        }
    }
}
