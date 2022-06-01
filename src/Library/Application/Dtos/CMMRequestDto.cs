using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos
{
    public class CMMRequestDto
    {
        public string RequestedBy { get; set; }
        public string Entity { get; set; }
        public bool Published { get; set; }
        public IFormFile FILE { get; set; }
        public string Customer { get; set; }
        public string CMMNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public string Model { get; set; }
        public string ManualRev { get; set; }
        public DateTime? RevDate { get; set; }
        public string Aircraft { get; set; }
        public string JobNo { get; set; }
        public IEnumerable<string> IncorporatedSeatAssemblies { get; set; }
        public IEnumerable<string> ServiceBulletins { get; set; }
        public IEnumerable<string> Reference { get; set; }
        public string AircraftInstallation { get; set; }
        public string Config { get; set; }
        public string TrimFinish { get; set; }
        public string Comments { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> SeatPartNumbers { get; set; }
        public IEnumerable<string> SeatPartNumbersTSO { get; set; }
    }
}
