using Accounts.Test.TestQuery;
using Accounts.Test.TestQuery.Dto.Term;
using Accounts.Test.TestQuery.Dto.Term.Dto;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.Test.TestQuery.Query
{
    public class TermQuery
    {
        public static string GetTerm()
        {
            // var id = 1;
            var DbContext = QueryBuilder.AccountDatabase();
            string parameters = QueryBuilder.ParameterData();
            var queryParameter = JsonConvert.DeserializeObject<TermQueryParameters>(parameters);
            var inputId = queryParameter.Id;
            var query = DbContext.Terms.Where(x => x.Id == inputId).Select(x => new TermDto
            {
                Name = x.Name,
                Id = x.Id
            }).FirstOrDefault();

            var result = new GetTerm()
            {
                result = query
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string GetAllTerm()
        {
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Terms.Select(x => new TermDto
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            int count = query.Count();

            var result = new GetAllTerm()
            {
                result = new GetAllTermResult()
                {
                    TotalCount = count,
                    items = query
                }
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}