using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ParkingLotController : Controller
    {
        private readonly IFloorService _floorService;

        public ParkingLotController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var floorViewModel = new FloorsViewModel();
            floorViewModel.Floors = _floorService.GetAllFloors();
            return View(floorViewModel);
        }

        [HttpGet]
        public ActionResult Data()
        {
            var floorViewModel = new FloorsViewModel();
            floorViewModel.Floors = _floorService.GetAllFloors();
            return PartialView(floorViewModel);
        }

        [HttpPost]
        public ActionResult SuggestFloor(string licensePlateNumber)
        {
            Floor floor;
            var result = _floorService.FindParkFloor(licensePlateNumber, out floor);
            
            return Json(new { isSuccess = result, suggestedFloor = floor });
        }

        [HttpPost]
        public ActionResult ParkCar(Guid floorId, string licensePlateNumber)
        {
            Floor floor;
            var result = _floorService.ParkCar(floorId, licensePlateNumber);

            return Json(new { isSuccess = result });
        }
    }
}