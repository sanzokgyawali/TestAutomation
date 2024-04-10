
using Accounts.Test.TestQuery.Dto.Company.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Company
{
    public class GetAllCompany : CommonDto
    {
        public GetAllCompanyResult Result { get; set; }
    }

    public class GetAllCompanyResult
    {
        public GetAllCompanyResult()
        {
            Items = new List<CompanyDto>();
        }

        public int TotalCount { get; set; }
        public List<CompanyDto> Items { get; set; }
    }
}