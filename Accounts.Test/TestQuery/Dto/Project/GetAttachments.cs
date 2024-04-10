
using Accounts.Test.TestQuery.Dto.Attachments.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Project
{
    public class GetAttachments : CommonDto
    {
        public List<AttachmentDto> result { get; set; }
    }
}