﻿using AutoMapper;
using Gamayun.Infrastucture.Entities;

namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class UserRMProfile : Profile
    {
        public UserRMProfile()
        {
            CreateMap<Student, UserRM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<Admin, UserRM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<Teacher, UserRM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));
        }
    }
}
