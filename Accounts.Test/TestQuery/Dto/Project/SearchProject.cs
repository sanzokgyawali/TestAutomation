using Accounts.Test.TestQuery.Dto.Project.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Project
{
    public class SearchProject : CommonDto

    {
        public SearchProjectResult Result { get; set; }
    }

    public class SearchProjectResult
    {
        public SearchProjectResult()
        {
            results = new List<ProjectListItemDto>();
        }

        public List<ProjectListItemDto> results { get; set; }
    }
}