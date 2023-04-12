using Domain.Com;

namespace Domain
{
    public class DeviceController
    {
        private ComHelper _comHelper = new ComHelper();

        private AutoResetEvent _limsiArrived = new AutoResetEvent(false);

        private string? _deviceLimsi;

        public void Open(string port, int baudRate)
        {
            _comHelper.DataReceived = ReplyArrived;
            _comHelper.DataReceived += LimsiArrived;
            if (_comHelper.Port != port || _comHelper.BaudRate != baudRate)
            {
                _comHelper.Port = port;
                _comHelper.BaudRate = baudRate;
                _comHelper.Open();
            }
        }

        public Action<string> ReplyArrived { get; set; } = null!;

        public async Task SendMsg(ControlMessage msg)
        {
            if (msg.Sn is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.SerialNumber, msg.Sn));
            if (msg.Ip is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ServerIp, msg.Ip));
            if (msg.Port is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ServerPort, msg.Port.ToString() ?? throw new ArgumentNullException("wrong port type")));
            if (msg.Username is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.Username, msg.Username));
            if (msg.Password is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.Password, msg.Password));
            if (msg.ApnIn is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ApnIn, msg.ApnIn));
            if (msg.ApnUsername is not null)
                await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ApnUsername, await GetLIMSI() + msg.ApnUsername));
            if (msg.ApnPassword is not null) await _comHelper.Write(ProxyWrapper.WithPayload(MessageType.ApnPassword, msg.ApnPassword));
        }

        public async Task Set(MessageType type)
        {
            if (type == MessageType.Save || type == MessageType.Reset)
                await _comHelper.Write(ProxyWrapper.WithConstByte(type));
            else throw new Exception("set with wrong message type");
        }

        public async Task<string> GetLIMSI()
        {
            await _comHelper.Write(ProxyWrapper.WithConstByte(MessageType.IMSI));
            if (_limsiArrived.WaitOne(TimeSpan.FromSeconds(5)))
                return _deviceLimsi!;
            else throw new Exception("下位机未响应LIMSI");
        }

        public void LimsiArrived(string raw)
        {
            if (!string.IsNullOrEmpty(raw) && raw.Contains("IMSI"))
            {
                _limsiArrived.Set();
                var imsi = raw.Split(':', 2)[1];
                _deviceLimsi = imsi.Remove(imsi.Length - 2);
            }
        }
    }

    public record class ControlMessage(string? Sn, string? Ip, int? Port, string? Username,
        string? Password, string? ApnIn, string? ApnUsername, string? ApnPassword);
}