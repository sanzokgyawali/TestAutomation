
using Accounts.Test.TestQuery.Dto.Term.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Term
{
    public class GetAllTerm : CommonDto
    {
        public GetAllTermResult result { get; set; }
    }

    public class GetAllTermResult
    {
        public GetAllTermResult()
        {
            items = new List<TermDto>();
        }

        public int TotalCount { get; set; }
        public List<TermDto> items { get; set; }
    }
}