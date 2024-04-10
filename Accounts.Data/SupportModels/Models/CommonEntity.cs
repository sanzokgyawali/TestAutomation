using System;
using System.Collections.Generic;
using System.Text;

namespace SupportingApplication.Data.Models
{
    public class CommonEntity
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
