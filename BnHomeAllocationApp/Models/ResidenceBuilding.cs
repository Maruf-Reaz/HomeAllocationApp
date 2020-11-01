using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models
{
    public class ResidenceBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ResidenceZoneId { get; set; }
        public ResidenceZone ResidenceZone { get; set; }

        public int BuildingTypeId { get; set; }
        public BuildingType BuildingType { get; set; }

        
    }
}
