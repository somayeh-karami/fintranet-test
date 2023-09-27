using congestion.calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Interfaces
{
    public interface IVehicle
    {
        public VehicleType VehicleType { get; set; }
       // string GetVehicleType();
        bool IsTollFree();
    }
}