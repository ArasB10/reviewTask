using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ParkingLotController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}