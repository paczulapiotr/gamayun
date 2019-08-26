using AutoMapper;
using Gamayun.Infrastucture.Entities;
using Gamayun.UI.Areas.Admin.Models;

namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class SemesterRMProfile : Profile
    {
        public SemesterRMProfile()
        {
            CreateMap<Semester, SemesterRM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString("yy/MM/dd hh:mm")))
                .ForMember(dest => dest.FinishedOn, opt => opt.MapFrom(src => src.FinishedOn.ToString("yy/MM/dd hh:mm")))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsObsolete, opt => opt.MapFrom(src => src.IsObsolete));

            CreateMap<Semester, SemesterVm>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString("yy/MM/dd hh:mm")))
                .ForMember(dest => dest.FinishedOn, opt => opt.MapFrom(src => src.FinishedOn.ToString("yy/MM/dd hh:mm")))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsObsolete, opt => opt.MapFrom(src => src.IsObsolete));
        }
    }
}
