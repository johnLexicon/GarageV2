using AutoMapper;
using GarageV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageV2.Models
{
    /// <summary>
    /// Class that is retrieved by automapper.
    /// Automapper searchs for a Profile class in the Models folder by convention.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ParkedVehicle, ParkedCarViewModel>();
            CreateMap<ParkedVehicle, DetailVehicleViewModel>();
            CreateMap<ParkedVehicle, ReceiptParkingViewModel>();
        }
    }
}
