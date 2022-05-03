using L2Backend.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Backend.Model
{
    public class Vehicle : IVehicle
    {
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
    }
}
