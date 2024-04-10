
using Accounts.Test.TestQuery.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Project.Dto
{
    public class ProjectQueryParameters : NamedQueryParameter
    {
        public bool? IsProjectActive { get; set; }

        public string Keyword { get; set; }

        public int? InvoiceCyclesId { get; set; }

        public int? TermId { get; set; }
    }
}