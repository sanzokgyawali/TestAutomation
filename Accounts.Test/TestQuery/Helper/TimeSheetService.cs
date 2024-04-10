using Accounts.Data.AccountModels;
using Accounts.Test.TestQuery.Enums;
using Accounts.Test.TestQuery.ExtensionMethod;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Test.TestQuery.Dto.Attachments.Dto.Helper
{
    public class TimeSheetService
    {
        public static double CalculateTotalHours(IEnumerable<HourLogEntries> hourLogEntries)
        {
            return hourLogEntries.DistinctBy(x => x.Day).Sum(x => x.Hours.HasValue ? x.Hours.Value : 0);
        }

        public Tuple<DateTime, DateTime> CalculateTimesheetPeriod(Projects project, Timesheets lastTimesheet)
        {
            return CalculateTimesheetPeriod(project.StartDt, project.EndDt, project.InvoiceCycleStartDt.Value, (InvoiceCyclesEnum)project.InvoiceCycleId, lastTimesheet?.EndDt);
        }

        public static Tuple<DateTime, DateTime> CalculateTimesheetPeriod(DateTime projectStartDt, DateTime? projectEndDt, DateTime invoiceCycleStartDt, InvoiceCyclesEnum invoiceCycles, DateTime? lastTimesheetEndDt)
        {
            DateTime? startDt;
            DateTime? endDt;
            DateTime CalculateTimesheetEndDt(DateTime dateTime)
            {
                var eom = CalculateEndDtBasedonInvoiceCycle(dateTime, invoiceCycles);
                return projectEndDt.HasValue ? projectEndDt > eom ? eom : projectEndDt.Value : eom;
            }

            if (!lastTimesheetEndDt.HasValue)
            {
                startDt = CalculateFirstTimesheetStartDt(projectStartDt, invoiceCycleStartDt, invoiceCycles);
            }
            else
            {
                startDt = lastTimesheetEndDt.Value.AddDays(1);
            }

            endDt = CalculateTimesheetEndDt(startDt.Value);
            return Tuple.Create(startDt.Value.Date, endDt.Value.Date);
        }

        public int BusinessDaysUntil(DateTime dtmStart, DateTime dtmEnd, params DateTime[] bankHolidays)
        {
            if (dtmStart > dtmEnd)
            {
                DateTime temp = dtmStart;
                dtmStart = dtmEnd;
                dtmEnd = temp;
            }

            /* Move border dates to the monday of the first full week and sunday of the last week */
            DateTime startMonday = dtmStart;
            int startDays = 1;
            while (startMonday.DayOfWeek != DayOfWeek.Monday)
            {
                if (startMonday.DayOfWeek != DayOfWeek.Saturday && startMonday.DayOfWeek != DayOfWeek.Sunday)
                {
                    startDays++;
                }
                startMonday = startMonday.AddDays(1);
            }

            DateTime endSunday = dtmEnd;
            int endDays = 0;
            while (endSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                if (endSunday.DayOfWeek != DayOfWeek.Saturday && endSunday.DayOfWeek != DayOfWeek.Sunday)
                {
                    endDays++;
                }
                endSunday = endSunday.AddDays(1);
            }

            int weekDays;

            /* calculate weeks between full week border dates and fix the offset created by moving the border dates */
            weekDays = (Math.Max(0, (int)Math.Ceiling((endSunday - startMonday).TotalDays + 1)) / 7 * 5) + startDays - endDays;

            if (dtmEnd.DayOfWeek == DayOfWeek.Saturday || dtmEnd.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDays -= 1;
            }

            return weekDays;
        }

        public bool AllTimesheetHoursEntered(DateTime startDt, DateTime endDt, IEnumerable<HourLogEntries> hourLogEntries)
        {
            var billableDays = BusinessDaysUntil(startDt, endDt);
            if (hourLogEntries.Count() < billableDays) return false;
            return true;
        }

        private static DateTime CalculateEndDtBasedonInvoiceCycle(DateTime dateTime, InvoiceCyclesEnum invoiceCycles)
        {
            switch (invoiceCycles)
            {
                case InvoiceCyclesEnum.Weekly:
                    return dateTime.AddDays(6);

                case InvoiceCyclesEnum.Monthly:
                    return dateTime.EndofMonth();

                case InvoiceCyclesEnum.BiWeekly:
                    return dateTime.AddDays(13);

                default:
                    return dateTime.EndofMonth();
            }
        }

        private static DateTime CalculateFirstTimesheetStartDt(DateTime projectStartDt, DateTime invoiceCycleStartDt, InvoiceCyclesEnum invoiceCycle)
        {
            return invoiceCycleStartDt;
        }
    }
}