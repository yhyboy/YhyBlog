using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace WebPage.Test
{
    [Export(typeof(IMefTest1))]
    public class MefTest1 : BaseTest<int>, IMefTest1
    {
        public string ShowTest()
        {
            return "Test1";
        }

      
    }
}