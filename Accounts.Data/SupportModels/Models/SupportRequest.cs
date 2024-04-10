using System;
using System.Collections.Generic;
using System.Text;

namespace SupportingApplication.Data.Models
{
    public class SupportRequest : CommonEntity
    {
        public int Id { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int TechnologyTypeId { get; set; }
        public int UrgencyStatusId { get; set; }
        public int Rating { get; set; }
        public int CandidateId { get; set; }
        public int? SupporterId { get; set; }
        public int RequestStatusId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual TechnologyType TechnologyType { get; set; }
        public virtual UrgencyStatus UrgencyStatus { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public virtual Account Candidate { get; set; }
        public virtual Account Supporter { get; set; }
    }
}
