using System;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ParkingLotController : Controller
    {
        private readonly IFloorService _floorService;
        private readonly INumberPlateValidator _numberPlateValidator;
        private readonly ILogger _logger;

        public ParkingLotController(IFloorService floorService, INumberPlateValidator numberPlateValidator, ILogger<ParkingLotController> logger)
        {
            _floorService = floorService;
            _numberPlateValidator = numberPlateValidator;
            _logger = logger;
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
        public ActionResult SuggestFloor(string licensePlateNumber, int entranceFloor)
        {
            if (!_numberPlateValidator.Validate(licensePlateNumber))
            {
                return Json(new ParkingLotResponse(new []{"Not a valid plate number"}));
            }

            if (_floorService.CarNumberExists(licensePlateNumber))
            {
                return Json(new ParkingLotResponse(new[] { "Car already exists" }));
            }

            Floor floor;
            var result = _floorService.FindParkFloor(licensePlateNumber, out floor, entranceFloor);
            
            if (result)
            {
                return Json(new ParkingLotResponse { IsSuccess = true, SuggestedFloor = floor });
            }
            else
            {
                return Json(new ParkingLotResponse(new[] { "Parking is full" }));
            }
        }

        [HttpPost]
        public ActionResult ParkCar(Guid floorId, string licensePlateNumber)
        {
            var result = _floorService.ParkCar(floorId, licensePlateNumber);

            _logger.LogInformation($"Park car: License plate number: {licensePlateNumber}, isSuccessful: {result} Date: {DateTime.UtcNow}");

            return Json(new ParkingLotResponse { IsSuccess = result });
        }

        public class ParkingLotResponse
        {
            public ParkingLotResponse()
            {
               
            }
            public ParkingLotResponse(string [] errors)
            {
                Errors = new List<string>(errors);
                IsSuccess = false;
            }
            public bool IsSuccess { get; set; }
            public Floor SuggestedFloor { get; set; }
            public IList<string> Errors { get; set; }
        }
    }
}