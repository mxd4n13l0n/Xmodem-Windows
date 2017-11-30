using com.skyfoxdigital.xmodem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XModem_UnitTests
{
    [TestClass]
    public class TestXModem : TestCases
    {
        [TestMethod]
        public void testXmodem()
        {
            byte seq = 0;
            foreach (String test in tests.Keys)
            {
                byte[] word = Encoding.Default.GetBytes(test);
                XmodemPacket xmodem = new XmodemPacket(word, ++seq);
                byte[] expected = new byte[133];
                expected[0] = 0x01;

                expected[1] = seq;
                expected[2] = (byte)~seq;

                Array.Copy(word, 0, expected, 3, word.Length);
                byte[] dataPadded = new byte[128];
                Array.Copy(word, 0, dataPadded, 0, word.Length);


                MemoryStream memoStream = new MemoryStream(2);
                UInt16 crc16 = (UInt16)new CRC16Xmodem().getCRC(dataPadded);
                int expectedCRC = tests[test];
                memoStream.Write(BitConverter.GetBytes(crc16), 0, 2);
                byte[] crcArray = memoStream.ToArray();
                expected[131] = crcArray[1];
                expected[132] = crcArray[0];
                CollectionAssert.AreEqual(expected, xmodem.getBytes());
            }

        }
    }
}
