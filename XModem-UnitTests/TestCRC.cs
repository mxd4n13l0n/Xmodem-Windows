using com.skyfoxdigital.xmodem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XModem_UnitTests
{
    [TestClass]
    public class TestCRC : TestCases
    {
        [TestMethod]
        public void testCRC()
        {
            foreach (String testKey in tests.Keys)
            {
                byte[] data = Encoding.Default.GetBytes(testKey);
                int value = tests[testKey];
                Assert.AreEqual(value, new CRC16Xmodem().getCRC(data));
            }
        }
    }
}
