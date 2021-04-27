using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;

namespace Service
{
    public interface IFloorService
    {
        IList<Floor> GetAllFloors();
        bool FindParkFloor(string carNumber, out Floor resultFloor, int entranceFloor);
        bool ParkCar(Guid floorId, string carNumber);
        bool CarNumberExists(string carNumber);
    }

    public class FloorService: IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public IList<Floor> GetAllFloors()
        {
            return _floorRepository.GetAllFloors();
        }

        public bool FindParkFloor(string carNumber, out Floor resultFloor, int entranceFloor)
        {
            resultFloor = new Floor();

            var floors = _floorRepository.GetAllFloors().Where(FloorHasSpace).ToList();

            if (floors.Count == 0)
            {
                return false;
            }

            if (entranceFloor < 0)
            {
                resultFloor = floors.Aggregate((x, y) => Math.Abs(x.FloorNumber - entranceFloor) < Math.Abs(y.FloorNumber - entranceFloor) ? x : y);
            }
            else
            {
                resultFloor = floors.Aggregate((x, y) => Math.Abs(Math.Abs(x.FloorNumber) - entranceFloor) < Math.Abs(Math.Abs(y.FloorNumber) - entranceFloor) ? x : y);
            }

            return resultFloor != null;
        }

        public bool CarNumberExists(string carNumber)
        {
            var floors = _floorRepository.GetAllFloors();

            var result = floors.FirstOrDefault(floor => FloorHasCarNumber(floor, carNumber));
            return result != null;
        }

        public bool ParkCar(Guid floorId, string carNumber)
        {
            Floor floor;
            if (_floorRepository.TryGetFloor(floorId, out floor))
            {
                var freeSpace = floor.Spaces.First(x => x.IsFree);
                freeSpace.CarNumber = carNumber;
                freeSpace.IsFree = false;
                return true;
            }

            return false;
        }

        private bool FloorHasSpace(Floor floor)
        {
            return floor.Spaces.Count(spaces => spaces.IsFree) > 0;
        }

        private bool FloorHasCarNumber(Floor floor, string carNumber)
        {
            return floor.Spaces.Count(space => space.CarNumber == carNumber) > 0;
        }
    }
}
