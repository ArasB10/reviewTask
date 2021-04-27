using System;
using System.Collections.Generic;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ParkingLotUnitTests
{
    [TestClass]
    public class FloorServiceTests
    {
        [TestMethod]
        [DataRow("555FJN", true)]
        [DataRow("BOBJOB", false)]
        public void CarNumberExists_ReturnsCorrectResult(string carNumber, bool expectedResult)
        {
            var floorRepository = new FloorRepository();
            floorRepository.AddFloor(GetFloor(1, 1));
            var floorService = new FloorService(floorRepository);

            var result = floorService.CarNumberExists(carNumber);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow("BOBJOB", false)]
        public void FindParkFloor_ReturnsCorrectResult(string carNumber, bool expectedResult)
        {
            var floorRepository = new FloorRepository();
            floorRepository.AddFloor(GetFloor(1, 1));
            var floorService = new FloorService(floorRepository);

            Floor floor;
            var result = floorService.FindParkFloor(carNumber, out floor);
            Assert.AreEqual(expectedResult, result);
        }

        private Floor GetFloor(int floorNumber, int maxSpace)
        {
            var floor = new Floor();
            floor.Id = Guid.NewGuid();
            floor.FloorNumber = floorNumber;
            floor.MaxSpace = maxSpace;
            floor.Spaces = new List<Space>();
            floor.Spaces.Add(new Space()
            {
                CarNumber = "555FJN",
                IsFree = false
            });

            return floor;
        }
    }
}
