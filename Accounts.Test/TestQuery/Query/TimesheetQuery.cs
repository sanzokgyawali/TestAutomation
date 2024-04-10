using Accounts.Test.TestQuery.Dto.Timesheet;
using Accounts.Test.TestQuery.Dto.Timesheet.Dto;
using Accounts.Test.TestQuery.Enums;
using Accounts.Test.TestQuery.ExtensionMethod;
using Accounts.Test.TestQuery.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Test.TestQuery.Query
{
    public class TimesheetQuery
    {
        public static List<TimesheetListItemDto> TimeSheetQuery(TimesheetQueryParameters queryParameter)
        {
            queryParameter.ProjectId = 1;
            queryParameter.ConsultantId = (int?)null;
            queryParameter.CompanyId = (int?)null;

            if (!queryParameter.StartTime.HasValue)
            {
                queryParameter.StartTime = DateTime.UtcNow.AddMonths(-1).StartOfMonth();
                queryParameter.EndTime = DateTime.UtcNow;
            }
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Timesheets.Where(x => x.Id != null);
            query = queryParameter.ProjectId.HasValue ? query.Where(x => x.ProjectId == queryParameter.ProjectId.Value) : query;
            query = queryParameter.ConsultantId.HasValue ? query.Where(x => x.Project.ConsultantId == queryParameter.ConsultantId.Value) : query;
            query = queryParameter.CompanyId.HasValue ? query.Where(x => x.Project.CompanyId == queryParameter.CompanyId.Value) : query;
            query = queryParameter.StatusId != null && queryParameter.StatusId.Length > 0 ? query.Where(x => queryParameter.StatusId.Contains(x.StatusId)) : query;
            query = queryParameter.Name == "Invoiced" && queryParameter.StartTime.HasValue && queryParameter.EndTime.HasValue ? query.Where(x => x.StartDt >= queryParameter.StartTime && x.EndDt <= queryParameter.EndTime) : query;
            query.OrderBy(x => x.LastModificationTime);
            query.OrderBy(x => x.CreationTime);

            //var timesheetInfo = new TimesheetListItemDto();
            var t1 = query.Select(x => new TimesheetListItemDto
            {
                Id = x.Id,
                StatusId = x.StatusId,
                StartDt = x.StartDt,
                EndDt = x.EndDt,
                ProjectId = x.ProjectId,
                TotalHrs = x.TotalHrs,
                CreatedDt = x.CreationTime,
                CreatedByUserName = x.CreatorUser.Name,
                ApprovedDate = x.ApprovedDate.Value,
                ApprovedByUserName = x.ApprovedByUser.Name,

                Project = new ProjectDto()
                {
                    ConsultantId = x.Project.ConsultantId,
                    ConsultantName = x.Project.Consultant.FirstName + " " + x.Project.Consultant.LastName,
                    CompanyName = x.Project.Company.DisplayName,
                    Email = x.Project.Consultant.Email,
                    PhoneNumber = x.Project.Consultant.PhoneNumber,
                    StartDt = x.Project.StartDt,
                    EndDt = x.Project.EndDt,
                    InvoiceCycleName = x.Project.InvoiceCycle.Name,
                    InvoiceCycleStartDt = x.Project.InvoiceCycleStartDt.Value.Date,
                    TermId = x.Project.TermId,
                    InvoiceCycleId = x.Project.InvoiceCycleId,
                    DiscountType = (DiscountTypeEnums?)(x.Project.DiscountType.Value),
                    DiscountValue = x.Project.DiscountValue,
                    Rate = x.Project.Rate,
                    TimesheetStatus = (TimesheetStasusesEnum)x.Status.Id,
                    TotalHoursBilled = x.Project.Invoices.Sum(z => z.TotalHours),
                    TotalAmountBilled = x.Project.Invoices.Sum(z => z.Total),
                    Id = x.ProjectId
                },
            }).ToList();
            return t1;
        }

        public static string TimesheetMonthlyHourReport()
        {
            string parameters = QueryBuilder.ParameterData();
            var queryParameter = JsonConvert.DeserializeObject<TimesheetQueryParameters>(parameters);

            queryParameter.ProjectId = 1;
            queryParameter.ConsultantId = (int?)null;
            queryParameter.CompanyId = (int?)null;

            if (!queryParameter.StartTime.HasValue)
            {
                queryParameter.StartTime = DateTime.UtcNow.AddMonths(-1).StartOfMonth();
                queryParameter.EndTime = DateTime.UtcNow;
            }
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.Timesheets.Where(x => x.Id != null);
            query = queryParameter.ProjectId.HasValue ? query.Where(x => x.ProjectId == queryParameter.ProjectId.Value) : query;
            query = queryParameter.ConsultantId.HasValue ? query.Where(x => x.Project.ConsultantId == queryParameter.ConsultantId.Value) : query;
            query = queryParameter.CompanyId.HasValue ? query.Where(x => x.Project.CompanyId == queryParameter.CompanyId.Value) : query;
            query = queryParameter.StatusId != null && queryParameter.StatusId.Length > 0 ? query.Where(x => queryParameter.StatusId.Contains(x.StatusId)) : query;
            query = queryParameter.Name == "Invoiced" && queryParameter.StartTime.HasValue && queryParameter.EndTime.HasValue ? query.Where(x => x.StartDt >= queryParameter.StartTime && x.EndDt <= queryParameter.EndTime) : query;
            query.OrderBy(x => x.LastModificationTime);
            query.OrderBy(x => x.CreationTime);

            var result = (from t in query
                          select t.HourLogEntries).SelectMany(x => x).GetMonthlyReport();

            var monthlyReport = new TimesheetMonthlyHourReport()
            { Result = result.ToList() };

            var test = JsonConvert.SerializeObject(monthlyReport);
            return JsonConvert.SerializeObject(monthlyReport);
        }

        public static string GetTimesheet()
        {
            var SavedQueries = new List<TimesheetQueryParameters>
            {
                new TimesheetQueryParameters
                {
                    Name="Pending Apprv",
                    StatusId=new int[]{(int)TimesheetStasusesEnum.Created }
                },
                new TimesheetQueryParameters
                {
                    Name="Approved",
                    StatusId=new int[]{(int)TimesheetStasusesEnum.Approved }
                },
                new TimesheetQueryParameters
                {
                    Name="Invoiced",
                    StatusId=new int[]{ (int) TimesheetStasusesEnum.Invoiced }
                },
                new TimesheetQueryParameters
                {
                    Name="Rejected",
                    StatusId = new int[] { (int) TimesheetStasusesEnum.Rejected}
                }
            };
            string parameters = QueryBuilder.ParameterData();
            var input = JsonConvert.DeserializeObject<TimesheetQueryParameters>(parameters);
            var query = TimeSheetQuery(input);
            var queryParameters =
               input.IsActive && !string.IsNullOrEmpty(input.Name) ?
               SavedQueries.Select(x => new TimesheetQueryParameters
               {
                   IsActive = x.IsActive,
                   Name = x.Name,
                   CompanyId = x.CompanyId,
                   ConsultantId = x.ConsultantId,
                   ProjectId = x.ProjectId,
                   StatusId = x.StatusId,
                   StartTime = x.StartTime,
                   EndTime = x.EndTime
               }
               ).ToList() : new[] { input }.ToList();

            var result = new GetTimesheetresult()
            {
                results = query
            };

            var resultList = new GetTimesheet()
            {
                result = result
            };

            var test = JsonConvert.SerializeObject(result);

            return test;
        }
    }
}