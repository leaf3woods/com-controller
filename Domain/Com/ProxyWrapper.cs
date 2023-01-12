using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Com
{
    public class ProxyWrapper
    {
        public static byte[] FrameHeader => new byte[] { 0xA5, 0x5A };
        public static byte ConstByte => 0x01;

        public static byte[] WithPayload(MessageType type, string payload)
        {
            var buffer = Encoding.ASCII.GetBytes(payload);
            return FrameHeader.Concat(BitConverter.GetBytes((short)payload.Length))
                .Append((byte)type).Concat(buffer).ToArray();
        }

        public static byte[] WithPayload(MessageType type, params byte[] payload)
            => FrameHeader.Concat(BitConverter.GetBytes((short)payload.Length))
            .Append((byte)type).Concat(payload).ToArray();
        public static byte[] WithConstByte(MessageType type) => FrameHeader.Concat(new byte[] { 0x00, 0x01, (byte)type, ConstByte}).ToArray();
    }
    public enum MessageType : byte
    {
        SerialNumber = 0x01,
        ServerIp = 0x02,
        ServerPort = 0x03,
        UserName = 0x04,
        Password = 0x05,
        Save = 0x06,
        Reset = 0xFF
    }
}
