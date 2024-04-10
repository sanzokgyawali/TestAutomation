using System;
using System.Collections.Generic;
using System.Text;

namespace ITsutra.Api.Core
{
    public class OutputType
    {
        public string Output { get; set; }
        public string Type { get; set; }
    }
   public class TestLog
    {
        public static List<OutputType> output = new List<OutputType>();
        public void Log( string Type, string writeLine)
        {
            output.Add(new OutputType
            {
                Output=writeLine,
                Type=Type
            });
        }
    }
}
