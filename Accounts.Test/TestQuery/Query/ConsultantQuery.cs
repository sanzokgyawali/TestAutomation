using Accounts.Test.TestQuery.Dto.Consultant;
using Accounts.Test.TestQuery.Dto.Consultant.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.Test.TestQuery.Query
{
    public class ConsultantQuery
    {
        public static string ConsultantDetails()
        {
            var id = 1;
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Consultants.Where(x => x.Id == id).Select(x => new ConsultantDetailsResult
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id
            }).FirstOrDefault();

            var result = new ConsultantDetails()
            {
                result = query
            };

            var res = JsonConvert.SerializeObject(result);

            return res;
        }

        public static string ConsultantSearch()
        {
            var searchText = "Prajit";
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Consultants.Where(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText)).OrderBy(x => x.FirstName)
                        .Select(x => new ConsultantDetailsResult
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            Id = x.Id
                        }).ToList();

            var consultantSearchResult = new ConsultantSearchResult()
            { results = query };

            var result = new ConsultantSearch()
            {
                result = consultantSearchResult
            };

            var res = JsonConvert.SerializeObject(result);
            return res;
        }
    }
}