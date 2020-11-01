using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnHomeAllocationApp.Models
{
    public class OfficerResidenceInfo
    {
        public int Id { get; set; }
       
        public int OfficerId { get; set; }
        public Officer Officer { get; set; }

        public bool IsAllotted { get; set; }
        public bool HasAppliedForResidence { get; set; }
        public bool HasAppliedForResidenceChange { get; set; }
        public bool IsVancancyProbable { get; set; }

        public string Reason { get; set; }
        public string Preference { get; set; }

        public int? ResidenceId { get; set; }
        public Residence Residence { get; set; }

        public DateTime AllottementDate { get; set; }
        public DateTime ProbableVacancyDate { get; set; }
        public DateTime VacancyDate { get; set; }
        public string VacancyReason { get; set; }



        public DateTime ApplicationDate { get; set; }
        public DateTime JoiningDate { get; set; }

        public int? ApplicationTypeId { get; set; }
        public ApplicationType ApplicationType { get; set; }

        public string Remarks { get; set; }



    }
}
