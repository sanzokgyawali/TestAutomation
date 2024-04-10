using Accounts.Test.TestQuery;
using Accounts.Test.TestQuery.Dto.Company;
using Accounts.Test.TestQuery.Dto.Company.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.Test.TestQuery.Query
{
    public class CompanyQuery
    {
        public static string GetAllCompany()
        {
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Companies.Select(x => new CompanyDto
            {
                DisplayName = x.DisplayName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                TermName = x.Term.Name,
                Id = x.Id
            }).ToList();

            var res = new GetAllCompanyResult()
            {
                TotalCount = query.Count(),
                Items = query
            };
            var response = new GetAllCompany()
            {
                Result = res
            };

            return JsonConvert.SerializeObject(response);
        }

        public static string GetCompany()
        {
            int id = 1;
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Companies.Where(x => x.Id == id).Select(x => new CompanyDto
            {
                DisplayName = x.DisplayName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                TermName = x.Term.Name,
                Id = x.Id
            }).FirstOrDefault();
            var test = JsonConvert.SerializeObject(query);
            return JsonConvert.SerializeObject(query);
        }
    }
}