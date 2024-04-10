using Accounts.Test.TestQuery;
using Accounts.Test.TestQuery.Dto.Attachments.Dto.Helper;
using Accounts.Test.TestQuery.Dto.Hourlog;
using Accounts.Test.TestQuery.Dto.Hourlog.Dto;
using Accounts.Test.TestQuery.Enums;
using Accounts.Test.TestQuery.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Test.TestQuery.Query
{
    public class HourlogQuery
    {
        public static string ProjectHourMonthlyReport()
        {
            int projectId = 2;
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.HourLogEntries.Where(x => x.Timesheet != null && x.Timesheet.ApprovedDate.HasValue)
                .Select(x => new
                {
                    x.ProjectId,
                    ConsultantName = x.Project.Consultant.FirstName + " " + x.Project.Consultant.LastName,
                    x.Day,
                    x.Hours
                })
                .ToList();

            var test = (from h1 in query
                        group h1 by new { h1.ProjectId, h1.ConsultantName } into g
                        select new HourMonthlyReport
                        {
                            ProjectId = g.Key.ProjectId,
                            ConsultantName = g.Key.ConsultantName,

                            MonthlySummaries = (from mhl in g
                                                group mhl by new { mhl.Day.Month, mhl.Day.Year, } into mg
                                                select new MonthlySummary
                                                {
                                                    ProjectId = g.Key.ProjectId,
                                                    Month = mg.Key.Month,
                                                    Year = mg.Key.Year,
                                                    Value = mg.Sum(y => y.Hours ?? 0),
                                                }).ToList()
                        }).ToList();

            var quer = (from h1 in query
                        group h1 by new { h1.ProjectId, h1.ConsultantName } into g
                        select new HourMonthlyReport
                        {
                            ProjectId = g.Key.ProjectId,
                            ConsultantName = g.Key.ConsultantName,

                            MonthlySummaries = (from mhl in g
                                                group mhl by new { mhl.Day.Month, mhl.Day.Year, } into mg
                                                select new MonthlySummary
                                                {
                                                    ProjectId = g.Key.ProjectId,
                                                    Month = mg.Key.Month,
                                                    Year = mg.Key.Year,
                                                    Value = mg.Sum(y => y.Hours ?? 0),
                                                }).ToList()
                        }).ToList();

            var testt = DbContext.Projects.Where(x => x.Id != null).Select(x => new
            {
                x.Id,
                x.Consultant.FirstName,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                CompanyName = x.Company.DisplayName,
                x.EndDt
            }).ToList();

            var Finalresult = (from proj in testt
                               join p in quer on proj.Id equals p.ProjectId
                               orderby proj.FirstName
                               select new HourMonthlyReport
                               {
                                   ProjectId = proj.Id,
                                   ConsultantName = proj.ConsultantName,
                                   CompanyName = proj.CompanyName,
                                   IsProjectActive = proj.EndDt.HasValue ? proj.EndDt > DateTime.UtcNow : true,
                                   MonthlySummaries = p.MonthlySummaries
                               }).ToList();
            var result = new ProjectHourMonthlyReport
            {
                Result = Finalresult
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string PayRollHourLogReport()
        {
            int year = 2020;
            int month = 1;
            var startDay = new DateTime(year, month, 1);
            var endDay = startDay.AddMonths(1);
            var DbContext = QueryBuilder.AccountDatabase();
            var hourLogs = DbContext.HourLogEntries.Where(x => x.Day >= startDay && x.Day < endDay)
                .Select(y => new
                {
                    Day = y.Day,
                    ProjectId = y.ProjectId,
                    CompanyName = y.Project.Company.DisplayName,
                    ConsultantName = y.Project.Company.DisplayName,
                    Hours = y.Hours,
                    Status = y.Timesheet.Status.Name
                }).ToList();

            var activeProjects = DbContext.Projects.Where(p => p.StartDt <= endDay && (!p.EndDt.HasValue || p.EndDt >= startDay)).Select(x => new
            {
                x.Id,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                CompanyName = x.Company.DisplayName,
                x.EndDt
            }).ToList();
            var activeProjectsHourLogReports = (from ac in activeProjects
                                                join hl in hourLogs on ac.Id equals hl.ProjectId into ah
                                                from p in ah.DefaultIfEmpty()
                                                group ac by new { ac.Id, ConsultantName = ac.ConsultantName, CompanyName = ac.CompanyName, IsActive = ac.EndDt >= DateTime.Now || ac.EndDt == null }
                                                into ach
                                                select new PayRollHourLogReportResult
                                                {
                                                    ProjectId = ach.Key.Id,
                                                    CompanyName = ach.Key.CompanyName,
                                                    ConsultantName = ach.Key.ConsultantName,
                                                    IsActive = ach.Key.IsActive
                                                }).ToList();

            foreach (var projectHourLogReport in activeProjectsHourLogReports)
            {
                var dailyhourlogs = hourLogs.Where(x => x.ProjectId == projectHourLogReport.ProjectId)
                    .Select(y => new DailyHourLog()
                    {
                        Day = y.Day,
                        Hours = y.Hours,
                        Status = y.Status
                    });
                projectHourLogReport.DailyHourLogs.AddRange(dailyhourlogs);
            }

            var result = new PayRollHourLogReport()
            {
                Result = activeProjectsHourLogReports,
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string ProjectHourLog()
        {
            int year = 2020, month = 1;
            var startDt = new DateTime(year, month, 1);
            var endDt = startDt.AddMonths(1);
            var DbContext = QueryBuilder.AccountDatabase();
            var TimesheetRepository = DbContext.Timesheets.Where(x => x.Id != null && x.IsDeleted == false).ToList();
            var HourLogRepository = DbContext.HourLogEntries.Where(x => x.Id != null).ToList();
            var activeProjectsQuery = DbContext.Projects.Where(p => p.StartDt <= endDt && (!p.EndDt.HasValue || p.EndDt >= startDt));

            var lastTimesheetQuery = from t in TimesheetRepository
                                     where activeProjectsQuery.Any(x => x.Id == t.ProjectId)
                                     group t by t.ProjectId into g
                                     select g.OrderByDescending(x => x.EndDt).First();
            var lastApprovedTimeSheetQuery = from t in TimesheetRepository
                                             where activeProjectsQuery.Any(x => x.Id == t.ProjectId) && t.ApprovedDate.HasValue
                                             group t by t.ProjectId into g
                                             select g.OrderByDescending(x => x.EndDt).First();
            var lastInvoicedTimesheetQuery = from t in TimesheetRepository
                                             where activeProjectsQuery.Any(x => x.Id == t.ProjectId) && t.InvoiceGeneratedDate.HasValue
                                             group t by t.ProjectId into g
                                             select g.OrderByDescending(x => x.EndDt).First();

            var query = from log in HourLogRepository
                        where log.Day >= startDt && log.Day <= endDt && activeProjectsQuery.Any(x => x.Id == log.ProjectId)
                        group log by log.ProjectId into projectLogs
                        select new
                        {
                            ProjectId = projectLogs.Key,
                            HourLogEntries = projectLogs.Select(plog => new HourLogEntryDto
                            {
                                Day = plog.Day,
                                Hours = plog.Hours.HasValue ? plog.Hours.Value : 0,
                                ProjectId = plog.ProjectId,
                                IsAssociatedWithTimesheet = plog.TimesheetId.HasValue && plog.Timesheet.StatusId != (int)TimesheetStasusesEnum.Created ? true : false,
                                TimesheetStatusesId = plog.TimesheetId.HasValue ? plog.Timesheet.StatusId : (int)TimesheetStasusesEnum.TimeSheetOpen,
                                Id = plog.Id
                            })
                        };

            //mapping activeprojectQuery to ActiveProjects
            var activeProjects = activeProjectsQuery.Select(x => new ProjectDto
            {
                Id = x.Id,
                ConsultantId = x.ConsultantId,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                CompanyName = x.Company.DisplayName,
                CompanyId = x.CompanyId,
                Email = x.Company.Email,
                PhoneNumber = x.Company.PhoneNumber,
                StartDt = x.StartDt,
                EndDt = x.EndDt,
                InvoiceCycleName = x.InvoiceCycle.Name,
                InvoiceCycleStartDt = x.InvoiceCycleStartDt.Value.Date,
                TermId = x.TermId,
                InvoiceCycleId = x.InvoiceCycleId,
                DiscountType = (DiscountTypeEnums)x.DiscountType,
                DiscountValue = x.DiscountValue,
                Rate = x.Rate,
                TotalAmountBilled = x.Invoices.Sum(z => z.Total),
                TotalHoursBilled = x.Invoices.Sum(z => z.TotalHours),
            });

            var projectHourLogs = query.ToList();
            var lastApproved = lastApprovedTimeSheetQuery.ToList();
            var lastInvoiced = lastInvoicedTimesheetQuery.ToList();
            var lastTimesheets = lastTimesheetQuery.ToList();

            var result = activeProjects.AsParallel().Select(proj =>
            {
                var projectHourLog = projectHourLogs.FirstOrDefault(y => y.ProjectId == proj.Id);
                var projectLastTimesheet = lastTimesheets.FirstOrDefault(t => t != null && t.ProjectId == proj.Id);
                var projectLastApprovedTimesheet = lastApproved.FirstOrDefault(t => t != null && t.ProjectId == proj.Id);
                var projectLastInvoicedTimesheet = lastInvoiced.FirstOrDefault(t => t != null && t.ProjectId == proj.Id);
                var (uStartDt, uEndDt) = TimeSheetService.CalculateTimesheetPeriod(proj.StartDt, proj.EndDt, proj.InvoiceCycleStartDt, (InvoiceCyclesEnum)proj.InvoiceCycleId, projectLastTimesheet?.EndDt);
                var duedays = projectLastTimesheet != null ? Math.Ceiling((DateTime.UtcNow - uStartDt).TotalDays) : Math.Ceiling((DateTime.UtcNow - uEndDt).TotalDays);

                var upcomingTimesheetSummary = new TimesheetSummary
                {
                    StartDt = uStartDt,
                    EndDt = uEndDt,
                    TotalHrs = projectHourLog?.HourLogEntries.Where(x => x.Day >= uStartDt && x.Day <= uEndDt).Sum(x => x.Hours.HasValue ? x.Hours.Value : 0)
                };
                proj.UpcomingTimesheetSummary = upcomingTimesheetSummary;

                proj.LastApprovedDate = projectLastApprovedTimesheet?.ApprovedDate;
                proj.LastInvoicedDate = projectLastInvoicedTimesheet?.InvoiceGeneratedDate;

                proj.PastTimesheetDays = duedays > 0 ? duedays : 0;
                if (duedays > 0)
                {
                    proj.TimesheetStatus = TimesheetStasusesEnum.TimeSheetOpen;
                }
                else
                {
                    proj.TimesheetStatus = projectLastTimesheet != null
                    ? (TimesheetStasusesEnum)projectLastTimesheet.StatusId
                    : TimesheetStasusesEnum.TimeSheetOpen;
                }
                return new ProjectHourLogEntryDto
                {
                    Project = proj,
                    HourLogEntries = projectHourLog?.HourLogEntries ?? new List<HourLogEntryDto>()
                };
            });
            result.OrderBy(x => x.Project.ConsultantName);
            var listResult = result.ToList();

            var response = new ProjectHourLog()
            {
                result = listResult
            };

            return JsonConvert.SerializeObject(response);
        }

        public static string GetAllHourLog()
        {
            var DbContext = QueryBuilder.AccountDatabase();
            var query = DbContext.HourLogEntries.Select(x => new
              HourLogEntryDto()
            {
                ProjectId = x.ProjectId,
                Day = x.Day.Date,
                Hours = x.Hours,
                IsAssociatedWithTimesheet = x.TimesheetId.HasValue,
                TimesheetStatusesId = x.Timesheet.StatusId,
                Id = x.Id
            }).ToList();

            int count = query.Count();

            var result = new GetAllHourLogResult()
            {
                TotalCount = count,
                Items = query
            };

            var response = new GetAllHourLog()
            { Result = result };

            return JsonConvert.SerializeObject(result);
        }

        public static string GetHourLog()
        {
            var DbContext = QueryBuilder.AccountDatabase();
            int id = 1;
            var query = DbContext.HourLogEntries.Where(x => x.Id == id).Select(x => new HourLogEntryDto()
            {
                ProjectId = x.ProjectId,
                Day = x.Day.Date,
                Hours = x.Hours,
                IsAssociatedWithTimesheet = x.TimesheetId.HasValue,
                TimesheetStatusesId = x.Timesheet.StatusId,
                Id = x.Id
            }).FirstOrDefault();

            var response = new GetHourLog()
            {
                Result = query
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}