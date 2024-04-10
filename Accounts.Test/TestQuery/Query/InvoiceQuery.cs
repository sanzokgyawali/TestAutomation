using Accounts.Data.AccountModels;
using Accounts.Test.TestQuery.Dto.Attachments.Dto;
using Accounts.Test.TestQuery.Dto.Invoice;
using Accounts.Test.TestQuery.Dto.Invoice.Dto;
using Accounts.Test.TestQuery.Dto.LineItems;
using Accounts.Test.TestQuery.ExtensionMethod;
using Accounts.Test.TestQuery.Helper;

using Newtonsoft.Json;
using System;
using System.Linq;

namespace Accounts.Test.TestQuery.Query
{
    public class InvoiceQuery
    {
        public static IQueryable<Invoices> GetInvoiceQuery(InvoiceQueryParameter queryParameter)
        {
            var DbContext = QueryBuilder.AccountDatabase();

            var query = DbContext.Invoices.Where(x => x.Id != null);

            query = !queryParameter.ConsultantName.IsNullOrWhiteSpace() ? query.Where(x => x.Consultant.FirstName.ToUpper().Contains(queryParameter.ConsultantName.ToUpper())) : query;
            query = queryParameter.ProjectId.HasValue ? query.Where(x => x.ProjectId == queryParameter.ProjectId.Value) : query;
            query = queryParameter.ConsultantId.HasValue ? query.Where(x => x.Project.ConsultantId == queryParameter.ConsultantId.Value) : query;
            query = queryParameter.CompanyId.HasValue ? query.Where(x => x.Project.CompanyId == queryParameter.CompanyId.Value) : query;
            query = queryParameter.StartDate.HasValue && queryParameter.EndDate.HasValue ? query.Where(p => p.InvoiceDate.Date >= queryParameter.StartDate && p.InvoiceDate.Date <= queryParameter.EndDate) : query;
            query = queryParameter.DueDate.HasValue ? query.Where(p => p.DueDate.Date == queryParameter.DueDate.Value) : query;
            query.OrderBy(x => x.Consultant.FirstName);
            var t = query.ToList();
            return query;
        }

        public static string SearchInvoice()
        {
            string parameters = QueryBuilder.ParameterData();
            var querayParameter = JsonConvert.DeserializeObject<InvoiceQueryParameter>(parameters);
            var queryParameter = new InvoiceQueryParameter();
            var query = GetInvoiceQuery(queryParameter);
            var result = query.Select(x => new InvoiceListItemDto
            {
                QBOInvoiceId = x.QboinvoiceId,
                CompanyName = x.Company.DisplayName,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                DueDate = x.DueDate,
                InvoiceDate = x.InvoiceDate,
                Total = x.Total
            }).ToList();

            var searchResult = new SearchInvoiceResult()
            {
                results = result
            };
            var response = new SearchInvoice()
            {
                result = searchResult
            };

            return JsonConvert.SerializeObject(response);
        }

        public static string GetInvoiceByMonthReport()
        {
            string parameters = QueryBuilder.ParameterData();
            var queryParametesr = JsonConvert.DeserializeObject<InvoiceQueryParameter>(parameters);
            var queryParameter = new InvoiceQueryParameter();
            var query = GetInvoiceQuery(queryParameter);
            var result = (from t1 in query
                          group t1 by new
                          {
                              t1.InvoiceDate.Month,
                              t1.InvoiceDate.Year
                          } into g

                          select new InvoiceMonthReportDto
                          {
                              Year = g.Key.Year,
                              MonthName = g.Key.Month,
                              MonthAmount = g.Sum(y => y.Total)
                          }).ToList();

            var response = new GetInvoiceByMonthReport()
            {
                result = result
            };
            return JsonConvert.SerializeObject(response);
        }

        public static string GetInvoice()
        {
            string parameters = QueryBuilder.ParameterData();
            // var Id = JsonConvert.DeserializeObject<int>(parameters);
            var DbContext = QueryBuilder.AccountDatabase();
            var Id = 1;
            var query = DbContext.Invoices.Where(x => x.Id == Id).Select(x => new InvoiceDto
            {
                TimesheetId = x.Timesheets.FirstOrDefault().Id,
                Description = x.Description,
                InvoiceDate = x.InvoiceDate,
                DueDate = x.DueDate,
                TotalHours = x.TotalHours,
                Rate = x.Rate,
                ServiceTotal = x.ServiceTotal,
                DiscountType = (DiscountTypeEnums?)x.DiscountType,
                DiscountValue = x.DiscountValue.HasValue ? (double)x.DiscountValue.Value : (double?)null,
                DiscountAmount = x.DiscountAmount,
                Total = x.Total,
                ConsultantName = x.Consultant.FirstName + " " + x.Consultant.LastName,
                TermName = x.Term.Name,
                CompanyEmail = x.Company.Email,
                QBOInvoiceId = x.QboinvoiceId,
                Attachments = x.Attachments.Where(y => y.InvoiceId == x.Id).Select(y => new AttachmentDto { Id = y.Id, ProjectId = y.ProjectId, Name = y.Name, OriginalName = y.OriginalName, ContentType = y.ContentType }).ToList(),
                Id = x.Id
            }).FirstOrDefault();

            var result = new GetInvoice()
            { result = query };

            return JsonConvert.SerializeObject(result);
        }

        public static Invoices InvoiceGenerator(int timesheetId, int userId, bool shouldAssociate = false) //utility module
        {
            var DbContext = QueryBuilder.AccountDatabase();
            var timesheet = DbContext.Timesheets.Where(x => x.Id == timesheetId).FirstOrDefault();
            if (timesheet.InvoiceId.HasValue)
            {
                throw new Exception("Invoice is already generated");
            }
            var generatedInvoice = new Invoices();

            generatedInvoice.TotalHours = timesheet.TotalHrs;
            generatedInvoice.ConsultantId = timesheet.Project.ConsultantId;
            generatedInvoice.CompanyId = timesheet.Project.CompanyId;
            generatedInvoice.TermId = timesheet.Project.TermId;
            generatedInvoice.Description = $"Billing Period {timesheet.StartDt.ToShortDateString()} -  {timesheet.EndDt.ToShortDateString()}";
            generatedInvoice.InvoiceDate = DateTime.Now;
            generatedInvoice.DueDate = DateTime.Now.AddDays(timesheet.Project.Term.DueDays);
            generatedInvoice.Rate = timesheet.Project.Rate;
            generatedInvoice.ProjectId = timesheet.ProjectId;
            generatedInvoice.Company = timesheet.Project.Company;
            generatedInvoice.Term = timesheet.Project.Term;
            generatedInvoice.Consultant = timesheet.Project.Consultant;
            generatedInvoice.Project = timesheet.Project;
            ;

            CalculateTotal(timesheet.Project, generatedInvoice);
            generatedInvoice.Attachments = timesheet.Attachments;
            return generatedInvoice;
        }

        public static void CalculateTotal(Projects project, Invoices invoice)
        {
            decimal discount = 0;

            invoice.ServiceTotal = System.Convert.ToDecimal(invoice.Rate * invoice.TotalHours);
            invoice.SubTotal = invoice.ServiceTotal + invoice.LineItems.Sum(x => x.Amount);
            switch (project.DiscountType)
            {
                case (int)DiscountTypeEnums.Percentage:

                    if (project.DiscountValue.HasValue)
                    {
                        discount = (project.DiscountValue.Value / 100) * invoice.SubTotal;
                    }
                    break;

                case (int)DiscountTypeEnums.Value:
                    if (project.DiscountValue.HasValue)
                    {
                        discount = project.DiscountValue.Value;
                    }
                    break;
            }

            invoice.DiscountType = project.DiscountType;
            invoice.DiscountValue = project.DiscountValue;
            invoice.DiscountAmount = Math.Round(discount, 2);
            invoice.Total = Math.Round(invoice.SubTotal - discount, 2);
        }

        public static string GenerateInvoice()
        {
            int timesheetId = 33;
            int currentUserId = 1;
            var invoice = InvoiceGenerator(timesheetId, currentUserId);
            var dto = new InvoiceDto();

            dto.Description = invoice.Description;
            dto.InvoiceDate = invoice.InvoiceDate;
            dto.DueDate = invoice.DueDate;
            dto.TotalHours = invoice.TotalHours;
            dto.Rate = invoice.Rate;
            dto.ServiceTotal = invoice.ServiceTotal;
            int? test = invoice.DiscountType;
            dto.SubTotal = invoice.SubTotal;
            dto.DiscountType = (DiscountTypeEnums?)invoice.DiscountType;
            dto.DiscountValue = invoice.DiscountValue.HasValue ? (double)invoice.DiscountValue.Value : (double?)null;
            dto.DiscountAmount = invoice.DiscountAmount;
            dto.Total = invoice.Total;
            dto.ConsultantName = invoice.Consultant.FirstName + " " + invoice.Consultant.LastName;
            dto.CompanyName = invoice.Company.DisplayName;
            dto.TermName = invoice.Term.Name;
            dto.CompanyEmail = invoice.Company.Email;
            dto.QBOInvoiceId = invoice.QboinvoiceId;
            dto.LineItems = invoice.LineItems.Select(x => new LineItemDto { Amount = x.Amount, Description = x.Description, ServiceDt = x.ServiceDt, ExpenseTypeName = x.ExpenseType.Name }).ToList();
            dto.Attachments = invoice.Attachments.Select(x => new AttachmentDto { Id = x.Id, ProjectId = x.ProjectId, Name = x.Name, OriginalName = x.OriginalName, ContentType = x.ContentType }).ToList();

            dto.TimesheetId = timesheetId;
            dto.Id = 0;
            var generateInvoice = new GenerateInvoice()
            {
                result = dto
            };
            var result = JsonConvert.SerializeObject(generateInvoice);
            return result;
        }
    }
}