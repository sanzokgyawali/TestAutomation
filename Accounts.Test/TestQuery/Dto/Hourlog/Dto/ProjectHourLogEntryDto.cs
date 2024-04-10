using Accounts.Test.TestQuery.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Hourlog.Dto
{
    public class ProjectHourLogEntryDto
    {
        public ProjectDto Project { get; set; }
        public IEnumerable<HourLogEntryDto> HourLogEntries { get; set; }
        public int Id { get; set; }
    }
}