using Library.Application.Data.CMM.Command;
using Library.Application.Dtos;
using Library.Domain.Entities;

namespace Library.Application.Profiles
{
    public class CMMProfile : AutoMapper.Profile
    {
        public CMMProfile()
        {
            CreateMap<CMM, CMMDto>()
                .ForMember(des => des.RevDate, opt => opt.MapFrom(s => s.ConvertedDate != null && s.ConvertedDate.Value.Year == 1 ? null : s.ConvertedDate))
                .ForMember(des => des.InitialDate, opt => opt.MapFrom(s => s.InitialDate!= null && s.InitialDate.Value.Year == 1 ? null : s.InitialDate))
                .ForMember(des => des.FILE, opt => opt.MapFrom(s => s.FILE));
            CreateMap<CreateCMM, CMM>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ConvertId, opt => opt.Ignore())
                .ForMember(d => d.vendor, opt => opt.Ignore())
                .ForMember(d => d.Engineer, opt => opt.Ignore())
                .ForMember(d => d.RFQ, opt => opt.Ignore())
                .ForMember(d => d.PM, opt => opt.Ignore())
                .ForMember(d => d.DocumentAvailable, opt => opt.Ignore())
                .ForMember(d => d.UnId, opt => opt.Ignore())
                .ForMember(d => d.ConvertedDate, opt => opt.Ignore())
                .ForMember(d => d.SeatsTSO, opt => opt.Ignore())
                .ForMember(d => d.Deleted, opt => opt.Ignore())
                .ForMember(d => d.FILE, opt=> opt.Ignore());
            CreateMap<CMMRequestDto, CMM>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ConvertId, opt => opt.Ignore())
                .ForMember(d => d.vendor, opt => opt.Ignore())
                .ForMember(d => d.Engineer, opt => opt.Ignore())
                .ForMember(d => d.RFQ, opt => opt.Ignore())
                .ForMember(d => d.PM, opt => opt.Ignore())
                .ForMember(d => d.DocumentAvailable, opt => opt.Ignore())
                .ForMember(d => d.UnId, opt => opt.Ignore())
                .ForMember(d => d.ConvertedDate, opt => opt.Ignore())
                .ForMember(d => d.SeatsTSO, opt => opt.Ignore())
                .ForMember(d => d.Deleted, opt => opt.Ignore())
                .ForMember(d => d.FILE, opt=> opt.Ignore());
        }
    }
}