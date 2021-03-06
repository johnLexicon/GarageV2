﻿using AutoMapper;
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
            CreateMap<ParkedVehicle, AddOrEditViewModel>();
            CreateMap<AddOrEditViewModel, ParkedVehicle>();
            CreateMap<Member, MemberAddOrEditViewModel>();
            CreateMap<MemberAddOrEditViewModel, Member>();
            CreateMap<MemberListViewModel, Member>();

            CreateMap<Member, DetailsMemberViewModel>()
                .ForMember(dest => dest.ParkedVehiclesInfo,
                            from => {
                                from.MapFrom(m => m.ParkedVehicles.Select(pv => new Tuple<int, string>(pv.Id, pv.RegNo)));
                            });
        }
    }
}
