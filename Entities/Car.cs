using congestion.calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.Interfaces;

namespace congestion.calculator.Entities
{
    public class Car : IVehicle
    {
        public VehicleType VehicleType { get; set; }

    

        public bool IsTollFree()
        {
            return Enum.IsDefined(typeof(VehicleType), "Car");
        }
    }
}