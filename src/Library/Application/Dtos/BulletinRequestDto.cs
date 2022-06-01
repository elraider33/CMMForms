using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Library.Application.Dtos
{
    public class BulletinRequestDto
    {
        public string RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public bool TSORequired { get; set; }
        public string Entity { get; set; }
        public bool Published { get; set; }
        public IFormFile FILE { get; set; }
        public string Sbno { get; set; }
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public string Customer { get; set; }
        public string ManualRev { get; set; }
        public DateTime? RevDate { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Aircraft { get; set; }
        public string CMMStatus { get; set; }
        // Cambiar por CMM Entity
        public List<string> CMM { get; set; }
        public string JobNumber { get; set; }
        public string RepairStationNumber { get; set; }
        public DateTime? RequiredCompletionDate { get; set; }
        public string Writer { get; set; }
        public List<string> SeatPartNumbers { get; set; }
        public bool? OnDisk { get; set; }
        public string Comments { get; set; }
        public List<string> Roles { get; set; }
    }
}