using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos
{
    public class CMMDto
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public string Entity { get; set; }
        public bool Published { get; set; }
        public FileDto FILE { get; set; }
        public string Customer { get; set; }
        public string CMMNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public string Model { get; set; }
        public string ManualRev { get; set; }
        public DateTime? RevDate { get; set; }
        public string Aircraft { get; set; }
        public string JobNo { get; set; }
        public string vendor { get; set; }
        public string Engineer { get; set; }
        public string RFQ { get; set; }
        public string PM { get; set; }
        public IEnumerable<string> IncorporatedSeatAssemblies { get; set; }
        public IEnumerable<string> ServiceBulletins { get; set; }
        public IEnumerable<string> Reference { get; set; }
        public string AircraftInstallation { get; set; }
        public string Config { get; set; }
        public string TrimFinish { get; set; }
        public string Comments { get; set; }
        public string DocumentAvailable { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> SeatPartNumbers { get; set; }
        public IEnumerable<string> SeatPartNumbersTSO { get; set; }

        public string UnId { get; set; }
        public DateTime? ConvertedDate { get; set; }
    }
}
