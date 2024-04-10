using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Timesheet.Dto
{
    public class GetTimesheetresult
    {
        public GetTimesheetresult()
        {
            results = new List<TimesheetListItemDto>();
        }

        public List<TimesheetListItemDto> results { get; set; }
    }
}