using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models.ViewModels
{
    public class OfficerRosterViewModel
    {
        public int Id { get; set; }
        public int OfficerId { get; set; }
        public int RankId { get; set; }
        public string NameAndRank { get; set; }
        public string PNO { get; set; }
        public DateTime CommissionDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int CommissionServicePoint { get; set; }
        public int RankServicePoint { get; set; }
        public int WaitingPeriod { get; set; }
        public int TotalPoint { get; set; }
        public int Position { get; set; }
        public int Children { get; set; }
        public string MobileNo { get; set; }
        public string WorkPlace { get; set; }
        public string Preference { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }

    }
}
