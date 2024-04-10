using Accounts.Test.TestQuery.Dto.Hourlog.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog
{
    public class ProjectHourLog : CommonDto
    {
        public List<ProjectHourLogEntryDto> result { get; set; }
    }
}