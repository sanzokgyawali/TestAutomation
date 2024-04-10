using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Accounts.Test.TestQuery.Helper
{
    internal class Sort<T, TKey>
    {
        public bool Condition { get; set; }
        public Expression<Func<T, TKey>> Expression { get; set; }
        public bool ByDescending { get; set; }
        public int Priority { get; set; }
    }
}