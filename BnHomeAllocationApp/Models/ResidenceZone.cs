using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models
{
    public class ResidenceZone
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<ResidenceBuilding> ResidenceBuildings { get; set; }

    }
}
