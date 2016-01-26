using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaggerTests
{
    [TestClass]
    public class NagTests
    {

        [TestMethod]
        public void CanActuallyTestItTest()
        {
            int b = 3;
            Assert.IsTrue(b == 3);
        }
    
    }
}
