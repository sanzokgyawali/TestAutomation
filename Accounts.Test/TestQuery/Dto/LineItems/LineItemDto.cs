﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.Dto.LineItems
{
    public class LineItemDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ServiceDt { get; set; }
        public string ExpenseTypeName { get; set; }
    }
}