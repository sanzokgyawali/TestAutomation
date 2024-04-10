using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Consultant.Dto
{
    public class ConsultantSearchResult
    {
        public ConsultantSearchResult()
        {
            results = new List<ConsultantDetailsResult>();
        }

        public List<ConsultantDetailsResult> results;
    }
}