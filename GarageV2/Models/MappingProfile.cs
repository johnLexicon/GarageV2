using System;
using AutoMapper;
using GarageV2.ViewModels;

namespace GarageV2.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ParkedVehicle, VehicleDetailsViewModel>();
        }
    }
}
