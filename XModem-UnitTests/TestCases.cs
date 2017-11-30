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
    public abstract class TestCases
    {
        protected CRC16Xmodem crc;
        protected Dictionary<String, int> tests;

        [TestInitialize]
        public void build()
        {
            crc = new CRC16Xmodem();
            //https://www.lammertbies.nl/comm/info/crc-calculation.html
            tests = new Dictionary<String, int>();
            tests.Add(
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                0x1CCE);
            /*
            tests.Add("hello", 0xC362);
            tests.Add("123456789", 0x31C3);
            tests.Add("ABCDE", 0xA559);
            tests.Add("abcde", 0x3EE1);
            tests.Add("Skyfox Digital", 0xFAF4);
            tests.Add(" ", 0x2462);
            tests.Add("!@", 0x7D13);
            tests.Add("I love java", 0x6CE3);
            tests.Add("mepos", 0x50D9);
            tests.Add("1,2,3,4,5", 0xD423);
            */
        }
    }
}
