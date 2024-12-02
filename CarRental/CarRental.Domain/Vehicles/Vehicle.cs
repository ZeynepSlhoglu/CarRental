using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.CarRental.Domain.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; set; } //guid olcak
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
    }
}
