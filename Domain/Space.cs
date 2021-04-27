using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Space
    {
        public bool IsFree { get; set; } = true;
        public bool HaveElectricSupport { get; set; } = false;
        public string CarNumber { get; set; }
    }
}
