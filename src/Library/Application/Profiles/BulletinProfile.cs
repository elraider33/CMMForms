using System;
using System.Linq;
using AutoMapper;
using Library.Application.Data.Bulletin.Command;
using Library.Application.Dtos;
using Library.Domain.Entities;

namespace Library.Application.Profiles
{
    public class BulletinProfile : AutoMapper.Profile
    {
        public BulletinProfile()
        {
            CreateMap<File, FileDto>();
            CreateMap<Bulletin, BulletinDto>()
                .ForMember(des => des.Aircraft, opt => opt.MapFrom(s => checkIfStringNull(s.Aircraft) ))
                .ForMember(des => des.Companycode, opt => opt.MapFrom(s => s.Roles))
                .ForMember(des => des.Customer, opt => opt.MapFrom(s => checkIfStringNull(s.Customer)))
                .ForMember(des => des.Description, opt => opt.MapFrom(s => checkIfStringNull(s.Description)))
                .ForMember(des => des.Entity, opt => opt.MapFrom(s => s.Entity))
                .ForMember(des => des.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(des => des.Manual, opt => opt.MapFrom(s => checkIfStringNull(s.Manual)))
                .ForMember(des => des.Model, opt => opt.MapFrom(s => checkIfStringNull(s.Model)))
                .ForMember(des => des.Sbno, opt => opt.MapFrom(s => checkIfStringNull(s.Sbno)))
                .ForMember(des => des.Type, opt => opt.MapFrom(s => checkIfStringNull(s.Type)))
                .ForMember(des => des.Unid, opt => opt.MapFrom(s => checkIfStringNull(s.Unid)))
                .ForMember(des => des.ManualRev, opt => opt.MapFrom(s => checkIfStringNull(s.ManualRev)))
                .ForMember(des => des.ModelNumber, opt => opt.MapFrom(s => checkIfStringNull(s.ModelNumber)))
                .ForMember(des => des.RevDate, opt => opt.MapFrom(s => s.RevDate))
                .ForMember(des => des.InitialDate, opt => opt.MapFrom(s => s.InitialDate.HasValue && s.InitialDate.Value.Year==1 ? null: s.InitialDate    ))
                .ForMember(des => des.JobNumber, opt => opt.MapFrom(s => checkIfStringNull(s.JobNumber)))
                .ForMember(des => des.SeatPartNumbers, opt => opt.MapFrom(s => s.SeatPartNumbers))
                .ForMember(des => des.FILE, opt => opt.MapFrom(s => s.FILE));
            CreateMap<CreateBulletin, Bulletin>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ConvertId, opt => opt.Ignore())
                .ForMember(d => d.Unid, opt => opt.Ignore())
                .ForMember(d => d.Manual, opt => opt.Ignore())
                .ForMember(d => d.CMMStatus, opt => opt.Ignore())
                .ForMember(d => d.RequiredCompletionDate, opt => opt.Ignore())
                .ForMember(d => d.OnDisk, opt => opt.Ignore())
                .ForMember(d => d.ConvertedDate, opt => opt.Ignore())
                .ForMember(d => d.View, opt => opt.Ignore())
                .ForMember(d => d.Deleted, opt => opt.Ignore())
                .ConstructUsing((src, ctx) =>
                {
                    var bulletin = new Bulletin
                    {
                        Aircraft = src.Aircraft,
                        CMM = src.CMM[0] == "null" ? null : src.CMM[0].Split("\r\n").ToList(),
                        CMMStatus = string.Empty,
                        Comments = src.Comments,
                        Customer = src.Customer,
                        Description = checkIfStringNull(src.Description),
                        Entity = src.Entity,
                        JobNumber = checkIfStringNull(src.JobNumber),
                        InitialDate = src.InitialDate,
                        ManualRev = checkIfStringNull(src.ManualRev),
                        Model = checkIfStringNull(src.Model),
                        ModelNumber = checkIfStringNull(src.ModelNumber),
                        OnDisk = false,
                        Published = src.Published,
                        RepairStationNumber = checkIfStringNull(src.RepairStationNumber),
                        RequestedBy = src.RequestedBy,
                        RequestedOn = src.RequestedOn,
                        RequiredCompletionDate = DateTime.Now,
                        RevDate = src.RevDate,
                        Roles = src.Roles[0].Split("\r\n").ToList(),
                        Sbno = src.Sbno,
                        SeatPartNumbers = src.SeatPartNumbers[0] == null ? null : src.SeatPartNumbers,
                        TSORequired = src.TSORequired,
                        Type = checkIfStringNull(src.Type),
                        Writer = checkIfStringNull(src.Writer)

                    };
                    return bulletin;
                })
                .ForMember(d => d.FILE, opt=> opt.Ignore());
            CreateMap<BulletinRequestDto, Bulletin>()
                .ForMember(des => des.CMM, opt => opt.Ignore())
                .ForMember(des => des.SeatPartNumbers, opt => opt.Ignore())
                .ForMember(des => des.Roles, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ConvertId, opt => opt.Ignore())
                .ForMember(d => d.Unid, opt => opt.Ignore())
                .ForMember(d => d.Manual, opt => opt.Ignore())
                .ForMember(d => d.ConvertedDate, opt => opt.Ignore())
                .ForMember(d => d.View, opt => opt.Ignore())
                .ForMember(d => d.Deleted, opt => opt.Ignore())
                .ForMember(d => d.FILE, opt=> opt.Ignore())
                .AfterMap((src, bulletin) =>
                {
                    bulletin.CMM = src.CMM.Count == 1? src.CMM[0].Split("\r\n").ToList(): src.CMM;
                    bulletin.Roles =src.Roles.Count == 1? src.Roles[0].Split("\r\n").ToList(): src.Roles;
                    bulletin.SeatPartNumbers= src.SeatPartNumbers == null || src.SeatPartNumbers[0] == null ? null: src.SeatPartNumbers[0].Split("\r\n").ToList();
                } );
        }

        private string checkIfStringNull(string field) => field == "null" ? string.Empty : field;
    }
}