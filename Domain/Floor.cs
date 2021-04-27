using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Floor: BaseEntity
    {
        public int FloorNumber { get; set; }
        public int MaxSpace { get; set; }
        public IList<Space> Spaces { get; set; }

    }
}
