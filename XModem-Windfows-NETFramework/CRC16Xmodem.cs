
using System;
using System.Text;

namespace com.skyfoxdigital.xmodem
{
    public class CRC16Xmodem
    {
        public int getCRC(byte[] bytes)
        {
            int crc = 0x0000;          // initial value
            int polynomial = 0x1021;   // 0001 0000 0010 0001  (0, 5, 12)

            foreach (byte b in bytes)
            {
                for (int i = 0; i < 8; i++)
                {
                    bool bit = ((b >> (7 - i) & 1) == 1);
                    bool c15 = ((crc >> 15 & 1) == 1);
                    crc <<= 1;
                    if (c15 ^ bit) crc ^= polynomial;
                }
            }

            crc &= 0xffff;
            return crc;
        }

        public int getCRC(String word)
        {
            return this.getCRC(Encoding.Default.GetBytes(word));
        }

        public byte[] calCrc(byte[] buffer, int startPos, int count)
        {
            int crc = 0, i;
            byte[] crcHL = new byte[2];

            while (--count >= 0)
            {
                crc = crc ^ (int)buffer[startPos++] << 8;
                for (i = 0; i < 8; ++i)
                {
                    if ((crc & 0x8000) != 0) crc = crc << 1 ^ 0x1021;
                    else crc = crc << 1;
                }
            }
            crc &= 0xFFFF;

            crcHL[0] = (byte)((crc >> 8) & 0xFF);
            crcHL[1] = (byte)(crc & 0xFF);

            return crcHL;
        }

    }
}
