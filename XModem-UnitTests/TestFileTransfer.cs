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
    public class TestFileTransfer
    {

        [TestMethod]
        public void testBuildFileTransferWithPath()
        {
            byte[] data = File.ReadAllBytes("rubick.jpg");
            int size = data.Length;
            int numberOfExpectedPackages = (size / 128) + 1; //(+1 of EOT packet)
            if (size % 128 > 1)
            {
                numberOfExpectedPackages++;
            }
            FileTransfer fileTransfer = new FileTransfer();
            List<byte[]> transfer = fileTransfer.build("rubick.jpg");
            Console.WriteLine("transfer count: " + transfer.Count);
            Console.WriteLine("Expected packets: " + numberOfExpectedPackages);
            Assert.IsTrue(transfer.Count == numberOfExpectedPackages);
        }

        [TestMethod]
        public void testAppletFile()
        {
            byte[] data = File.ReadAllBytes("applet_flash_mepos_cnb0084_1.bin");
            int size = data.Length;
            int numberOfExpectedPackages = (size / 128) + 1; //(+1 of EOT packet)
            if (size % 128 > 1)
            {
                numberOfExpectedPackages++;
            }
            FileTransfer fileTransfer = new FileTransfer();
            List<byte[]> transfer = fileTransfer.build("applet_flash_mepos_cnb0084_1.bin");
            Console.WriteLine("transfer count: " + transfer.Count);
            Console.WriteLine("Expected packets: " + numberOfExpectedPackages);
            Assert.IsTrue(transfer.Count == numberOfExpectedPackages);
        }

    }
}
