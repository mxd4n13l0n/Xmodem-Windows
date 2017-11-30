using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.skyfoxdigital.xmodem
{
    public class XmodemPacket
    {
        private static int POS_START_OF_HEADER = 0;
        private static int POS_PACKET_NUM = 1;
        private static int POS_PACKET_NUM_INVERSE = 2;
        private static int POS_START_OF_DATA = 3;
        private static int POS_CRC_1 = 131;
        private static int POS_CRC_2 = 132;

        public static byte SOH = 0x01;
        private byte[] packet;

        public XmodemPacket(byte[] word, int packetNumber)
        {
            packet = new byte[133];
            int maxLength = word.Length > 128 ? 128 : word.Length;
            packet[POS_START_OF_HEADER] = SOH;
            packet[POS_PACKET_NUM] = (byte)packetNumber;
            packet[POS_PACKET_NUM_INVERSE] = (byte)~packetNumber;

            Array.Copy(word, 0, packet, POS_START_OF_DATA, maxLength);
            byte[] crcByteArray = new CRC16Xmodem().calCrc(packet, POS_START_OF_DATA, 128);
            packet[POS_CRC_1] = crcByteArray[0];
            packet[POS_CRC_2] = crcByteArray[1];
        }

        public byte[] getBytes()
        {
            return packet;
        }
    }
}
