using Domain.Com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DeviceController
    {
        private ComHelper _comHelper = new ComHelper();
        public void Open(string port, int baudRate)
        {
            _comHelper.DataReceived = ReplyArrived;
            if (_comHelper.Port != port || _comHelper.BaudRate!= baudRate)
            {
                _comHelper.Port = port;
                _comHelper.BaudRate = baudRate;
                _comHelper.Open();
            }
        }

        public Action<string> ReplyArrived { get; set; } = null!;
        public async Task SendMsg(ControlMessage msg)
        {
            if(msg.Sn is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.SerialNumber, msg.Sn));
            if(msg.Ip is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ServerIp, msg.Ip));
            if (msg.Port is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ServerPort, msg.Port.ToString() ?? throw new ArgumentNullException("wrong port type")));
            if (msg.UserName is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.UserName, msg.UserName));
            if (msg.Password is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.Password, msg.Password));
        }
        public async Task Set(MessageType type)
        {
            if (type == MessageType.Save || type == MessageType.Reset)
                await _comHelper.Write(ProxyWrapper.WithConstByte(type));
            else throw new Exception("set with wrong message type");
        }
    }

    public record class ControlMessage(string? Sn, string? Ip, int? Port, string? UserName, string? Password);
}
