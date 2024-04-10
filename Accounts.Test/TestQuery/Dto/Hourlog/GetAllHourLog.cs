using Accounts.Test.TestQuery.Dto.Hourlog.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog
{
    public class GetAllHourLog : CommonDto
    {
        public GetAllHourLogResult Result;
    }

    public class GetAllHourLogResult
    {
        public int TotalCount { get; set; }
        public List<HourLogEntryDto> Items;
    }
}