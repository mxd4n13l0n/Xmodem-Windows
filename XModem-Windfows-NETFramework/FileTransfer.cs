using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.skyfoxdigital.xmodem
{
    public class FileTransfer
    {
        private static byte[] END_OF_TRANSFER = new byte[] { 0x04 };

        
        public List<byte[]> build(string path)
        {
            return build(File.ReadAllBytes(path));
        }

        public List<byte[]> build(byte[] data)
        {
            
            MemoryStream memoryStream = new MemoryStream(data);
            return this.build(memoryStream);
            
        }

        public List<byte[]> build(MemoryStream memoryStream)
        {
            int sequence = 0;
            int offset = 0;
            List<byte[]> packetList = new List<byte[]>();
            byte[] buffer = new byte[128];
            while (memoryStream.Read(buffer, offset, buffer.Length ) > 0)
            {
                byte[] data = new byte[buffer.Length];
                Array.Copy(buffer, data, data.Length);
                packetList.Add(new XmodemPacket(data, ++sequence).getBytes());
            }
            packetList.Add(END_OF_TRANSFER);

            return packetList;
        }
    }
}
