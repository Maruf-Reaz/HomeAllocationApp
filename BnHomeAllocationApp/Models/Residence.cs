using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models
{
    public class Residence
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }

        public int ResidenceBuildingId { get; set; }
        public ResidenceBuilding ResidenceBuilding { get; set; }

        public bool IsAllotted { get; set; }

       
    }
}
