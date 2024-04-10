using Accounts.Test.TestQuery;
using Accounts.Test.TestQuery.Dto.Attachments.Dto;
using Accounts.Test.TestQuery.Dto.Attachments.Dto.Helper;
using Accounts.Test.TestQuery.Dto.Project;
using Accounts.Test.TestQuery.Dto.Project.Dto;
using Accounts.Test.TestQuery.Enums;
using Accounts.Test.TestQuery.ExtensionMethod;
using Accounts.Test.TestQuery.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Test.TestQuery.Query
{
    public class ProjectQuery
    {
        public static string GetAttachments()
        {
            int projectId = 1;
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Attachments.Where(x => x.ProjectId == projectId && x.TimesheetId == null)
                .Select(x => new Accounts.Test.TestQuery.Dto.Attachments.Dto.AttachmentDto
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    Name = x.Name,
                    OriginalName = x.OriginalName,
                    ContentType = x.ContentType
                }).ToList();
            var result = new GetAttachments()
            {
                result = query
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string SearchProject()
        {
            var SavedQueries = new List<ProjectQueryParameters>
            {
                new ProjectQueryParameters
                {
                    Name="Active",
                    IsProjectActive=true
                },
                 new ProjectQueryParameters
                {
                    Name="Inactive",
                    IsProjectActive=false
                 }
            };
            var DbContext = QueryBuilder.AccountDatabase();
            string parameters = QueryBuilder.ParameterData();
            var queryParameter = JsonConvert.DeserializeObject<ProjectQueryParameters>(parameters);
            var query = DbContext.Projects.Where(x => x.Id != null);
            query = queryParameter.IsProjectActive.HasValue ? query.Where(x => queryParameter.IsProjectActive.Value ? x.EndDt.HasValue ? x.EndDt > DateTime.UtcNow : true : x.EndDt.HasValue && x.EndDt < DateTime.UtcNow) : query;
            query = !queryParameter.Keyword.IsNullOrWhiteSpace() ? query.Where(x => x.Company.DisplayName.Contains(queryParameter.Keyword.ToUpper())) : query;
            query = queryParameter.InvoiceCyclesId.HasValue ? query.Where(x => x.InvoiceCycleId == queryParameter.InvoiceCyclesId) : query;
            query = queryParameter.TermId.HasValue ? query.Where(x => queryParameter.TermId == x.TermId) : query;
            query.OrderBy(x => x.StartDt);
            var queryParameters = SavedQueries.Select(x => new ProjectQueryParameters
            {
                IsProjectActive = queryParameter.IsProjectActive,
                Keyword = queryParameter.Keyword,
                InvoiceCyclesId = queryParameter.InvoiceCyclesId,
                TermId = queryParameter.TermId
            }).ToList();

            var projects = query.Select(x => new ProjectListItemDto
            {
                ConsultantId = x.ConsultantId,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                CompanyName = x.Company.DisplayName,
                CompanyId = x.CompanyId,
                StartDt = x.StartDt,
                EndDt = x.EndDt,
                InvoiceCycleStartDt = x.InvoiceCycleStartDt.Value,
                InvoiceCycleName = x.InvoiceCycle.Name,
                TermName = x.Term.Name,
                InvoiceCycleId = x.InvoiceCycleId,
                Rate = x.Rate,
                TotalHoursBilled = x.Invoices.Sum(y => y.TotalHours),
                TotalAmountBilled = x.Invoices.Sum(y => y.Total),
                Id = x.Id
            });

            var lastTimesheetQuery = from p in DbContext.Projects.Where(x => x.Id != null)
                                     let lT = p.Timesheets.OrderByDescending(x => x.EndDt).FirstOrDefault()
                                     where projects.Any(x => x.Id == p.Id)
                                     select lT;
            var lastTimesheets = lastTimesheetQuery.ToList();

            Parallel.ForEach(projects, proj =>
            {
                var projectLastTimesheet = lastTimesheets.FirstOrDefault(t => t != null && t.ProjectId == proj.Id);
                var (uStartDt, uEndDt) = TimeSheetService.CalculateTimesheetPeriod(proj.StartDt, proj.EndDt, proj.InvoiceCycleStartDt, (InvoiceCyclesEnum)proj.InvoiceCycleId, projectLastTimesheet?.EndDt);
                var duedays = projectLastTimesheet != null ? Math.Ceiling((DateTime.UtcNow - uStartDt).TotalDays) : Math.Ceiling((DateTime.UtcNow - uEndDt).TotalDays);
                proj.PastTimesheetDays = duedays > 0 ? duedays : 0;
            });
            var result = new SearchProjectResult()
            {
                results = projects.ToList()
            };
            var response = new SearchProject()
            { Result = result };

            var test = JsonConvert.SerializeObject(response);

            return JsonConvert.SerializeObject(response);
        }

        public static string GetProject()
        {
            int Id = 1;
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Projects.Where(x => x.Id == Id).Select(x => new ProjectDto
            {
                ConsultantId = x.ConsultantId,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                CompanyName = x.Company.DisplayName,
                CompanyId = x.CompanyId,
                Email = x.Consultant.Email,
                PhoneNumber = x.Consultant.PhoneNumber,
                StartDt = x.StartDt,
                EndDt = x.EndDt,
                InvoiceCycleName = x.InvoiceCycle.Name,
                InvoiceCycleStartDt = x.InvoiceCycleStartDt.Value.Date,
                TermId = x.TermId,
                InvoiceCycleId = x.InvoiceCycleId,
                DiscountType = (DiscountTypeEnums?)(x.DiscountType.Value),
                DiscountValue = x.DiscountValue,
                Rate = x.Rate,
                TimesheetStatus = (TimesheetStasusesEnum)x.Timesheets.Where(y => y.ProjectId == x.Id).FirstOrDefault().StatusId,
                TotalHoursBilled = x.Invoices.Sum(z => z.TotalHours),
                TotalAmountBilled = x.Invoices.Sum(z => z.Total),
                Id = x.Id
            }).FirstOrDefault();
            var result = new GetProject()
            { result = query };

            var test = JsonConvert.SerializeObject(result);
            return JsonConvert.SerializeObject(result);
        }
    }
}