using System;
using System.Collections.Generic;
using DataAccess;
using Domain;

namespace Web.Initializers
{
    public class DummyDataInitializer
    {
        private readonly IFloorRepository _floorRepository;

        public DummyDataInitializer(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public void CreateDummyData()
        {
            // Create test data here
            _floorRepository.AddFloor(GetFloor(1, 10));
            _floorRepository.AddFloor(GetFloor(2, 10));
            _floorRepository.AddFloor(GetFloor(3, 10));
        }

        private Floor GetFloor(int floorNumber, int maxSpace)
        {
            var floor = new Floor();
            floor.Id = Guid.NewGuid();
            floor.FloorNumber = floorNumber;
            floor.MaxSpace = maxSpace;
            floor.Spaces = new List<Space>();

            for (int i = 0; i < maxSpace; i++)
            {
                floor.Spaces.Add(new Space());
            }

            return floor;
        }
    }
}
