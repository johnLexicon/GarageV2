using System;
using AutoMapper;
using GarageV2.ViewModels;

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
            CreateMap<ParkedVehicle, VehicleDetailsViewModel>();
            CreateMap<ParkedVehicle, ParkedVehiclesViewModel>();
            CreateMap<ParkedVehicle, ReceiptViewModel>();
        }
    }
}
