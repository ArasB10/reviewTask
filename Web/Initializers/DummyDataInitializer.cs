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
            _floorRepository.AddFloor(GetFloor(-2, 15, 3,0));
            _floorRepository.AddFloor(GetFloor(-1, 15, 3,0));
            _floorRepository.AddFloor(GetFloor(1, 10, 10,2));
            _floorRepository.AddFloor(GetFloor(2, 10, 10,2));
            _floorRepository.AddFloor(GetFloor(3, 10, 10,1));
        }

        private Floor GetFloor(int floorNumber, int maxSpace, int initialCars, int evSpaceCount)
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
                        CarNumber = RandomString(6),
                        HaveElectricSupport = i < evSpaceCount
                    });
                }
                else
                {
                    floor.Spaces.Add(new Space()
                    {
                        IsFree = true,
                        HaveElectricSupport = i < evSpaceCount
                    });
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
            if (maxSpace == initialCars)
            {
                return Enumerable.Range(0, maxSpace).ToList();
            }

            List<int> initialCarPositions = new List<int>();
            Random random = new Random();
            int number;
            for (int i = 0; i < initialCars; i++)
            {
                do
                {
                    number = random.Next(1, maxSpace - 1);
                } while (initialCarPositions.Contains(number));

                initialCarPositions.Add(number);
            }

            return initialCarPositions;
        }
    }
}
