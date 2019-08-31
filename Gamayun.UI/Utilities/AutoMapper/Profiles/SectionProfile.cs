using AutoMapper;
using Gamayun.Infrastucture.Entities;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.UI.Areas.Student.Models;
using Gamayun.UI.Areas.Teacher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Utilities.AutoMapper.Profiles
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<Section, SectionRM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.State.ToString()))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic.Name));

            CreateMap<Section, SectionVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.State.ToString()))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic.Name))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Presences, opt => opt.Ignore())
                .ForMember(dest => dest.Dates, opt => opt.Ignore());

            CreateMap<Section, StudentSectionVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.State.ToString()))
                .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.Topic.Name))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade));
        }
    }
}
