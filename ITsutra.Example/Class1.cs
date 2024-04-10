using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ITsutra.Example
{
    [Binding]
    public class Class1
    {
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            var b = 9;
        }

    }
}
