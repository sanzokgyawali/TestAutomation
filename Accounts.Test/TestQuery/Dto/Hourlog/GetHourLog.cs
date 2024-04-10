using Accounts.Test.TestQuery.Dto.Hourlog.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog
{
    public class GetHourLog : CommonDto
    {
        public HourLogEntryDto Result { get; set; }
    }
}