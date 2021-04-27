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
        bool FindParkFloor(string carNumber, out Floor resultFloor);

        bool ParkCar(Guid floorId, string carNumber);
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

        public bool FindParkFloor(string carNumber, out Floor resultFloor)
        {
            resultFloor = new Floor();
            var floors = _floorRepository.GetAllFloors();
            resultFloor = floors.FirstOrDefault(FloorHasSpace);

            return resultFloor != null;
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
    }
}
