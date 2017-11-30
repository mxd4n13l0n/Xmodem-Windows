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
            StreamReader reader = new StreamReader(memoryStream);
            return this.build(reader);
        }

        public List<byte[]> build(StreamReader streamReader)
        {
            int sequence = 0;
            int offset = 0;
            List<byte[]> packetList = new List<byte[]>();
            char[] buffer = new char[128];
            while (streamReader.ReadBlock(buffer, offset, buffer.Length) > 0)
            {
                byte[] data = new byte[buffer.Length];
                byte[] bufferBytes = Encoding.Unicode.GetBytes(buffer);
                Array.Copy(bufferBytes, data, bufferBytes.Length);
                packetList.Add(new XmodemPacket(data, ++sequence).getBytes());
            }
            packetList.Add(END_OF_TRANSFER);
            streamReader.Dispose();
            return packetList;
        }
    }
}
