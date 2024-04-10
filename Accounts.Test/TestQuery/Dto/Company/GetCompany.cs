using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Company
{
    public class GetCompany : CommonDto
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string TermName { get; set; }

        public int Id { get; set; }
    }
}