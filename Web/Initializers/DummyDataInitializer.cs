using System;
using System.Collections.Generic;
using System.Linq;
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
            _floorRepository.AddFloor(GetFloor(1, 10, 5));
            _floorRepository.AddFloor(GetFloor(2, 10, 3));
            _floorRepository.AddFloor(GetFloor(3, 10, 3));
        }

        private Floor GetFloor(int floorNumber, int maxSpace, int initialCars)
        {
            var floor = new Floor();
            floor.Id = Guid.NewGuid();
            floor.FloorNumber = floorNumber;
            floor.MaxSpace = maxSpace;
            floor.Spaces = new List<Space>();
            var initialCarPositions = GetRandomPositions(maxSpace, initialCars);

            for (int i = 0; i < maxSpace; i++)
            {
                if (initialCarPositions.Contains(i))
                {
                    floor.Spaces.Add(new Space()
                    {
                        IsFree = false,
                        CarNumber = RandomString(6)
                    });
                }
                else
                {
                    floor.Spaces.Add(new Space());
                }
                
            }

            return floor;
        }

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private List<int> GetRandomPositions(int maxSpace, int initialCars)
        {
            List<int> initialCarPositions = new List<int>();
            Random random = new Random();
            for (int i = 0; i < initialCars; i++)
            {
                initialCarPositions.Add(random.Next(0, maxSpace-1));
            }

            return initialCarPositions;
        }
    }
}
