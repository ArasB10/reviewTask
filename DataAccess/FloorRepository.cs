using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DataAccess
{
    public interface IFloorRepository
    {
        IList<Floor> GetAllFloors();
        void AddFloor(Floor floor);
        bool TryGetFloor(Guid id, out Floor floor);
    }
    public class FloorRepository : Repository, IFloorRepository
    {
        public FloorRepository() : base()
        {

        }

        public IList<Floor> GetAllFloors()
        {
            return GetAll<Floor>().OrderBy(x => x.FloorNumber).ToList();
        }

        public void AddFloor(Floor floor)
        {
            Add(floor);
        }

        public bool TryGetFloor(Guid id, out Floor floor)
        {
            return TryGet<Floor>(id, out floor);
        }

    }
}
