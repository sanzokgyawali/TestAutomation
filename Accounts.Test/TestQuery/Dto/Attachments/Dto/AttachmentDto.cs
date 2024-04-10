using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.Attachments.Dto
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public string OriginalName { get; set; }

        public string ContentType { get; set; }
    }
}