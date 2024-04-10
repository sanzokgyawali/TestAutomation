using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Enums
{
    public enum TimesheetStasusesEnum
    {
        Created = 1,
        Approved = 2,
        Rejected = 3,
        Invoiced = 4,
        InvoiceGenerated = 5,
        TimeSheetOpen = 6,
        TimeSheetSubmitted = 7
    }

    public enum InvoiceCyclesEnum
    {
        Weekly = 1,
        Monthly = 2,
        BiWeekly = 3,
        BiMonthly = 4
    }
}