using AutoMapper;
using GarageV2.ViewModels;
using System;
using System.Linq;

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
            CreateMap<Member, RegisterViewModel>();

            //Mapping the Email from the viewmodel to the IdentityUser UserName.
            CreateMap<RegisterViewModel, Member>()
                .ForMember(dest => dest.UserName, from => from.MapFrom(src => src.Email));

            CreateMap<MemberListViewModel, Member>();

            CreateMap<EditMemberViewModel, Member>();
            CreateMap<Member, EditMemberViewModel>();

            CreateMap<Member, DetailsMemberViewModel>()
                .ForMember(dest => dest.ParkedVehiclesInfo,
                            from => {
                                from.MapFrom(m => m.ParkedVehicles.Select(pv => new Tuple<int, string>(pv.Id, pv.RegNo)));
                            });

        }
    }
}
