using System;
using System.Collections.Generic;
using GarageV2.Models;

namespace GarageV2.Services
{
    public interface IVehiclesData
    {
        ParkedVehicle AddVehicle(ParkedVehicle vehicle);
        IEnumerable<ParkedVehicle> GetAll();
        ParkedVehicle RemoveVehicle(int id);
    }
}
