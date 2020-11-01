using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models
{
    public class Officer
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string PNO { get; set; }

        public DateTime CommissionDate { get; set; }

        public string MobileNumber { get; set; }

        public string CurrentWorkPlace { get; set; }
        public int NumberOfChildren { get; set; }

        public int OfficerRankId { get; set; }
        public OfficerRank OfficerRank { get; set; }

    }
}
