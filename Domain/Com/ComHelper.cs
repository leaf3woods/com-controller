using System.IO.Ports;
using System.Text;

namespace Domain.Com
{
    public class ComHelper
    {
        private SerialPort _serialPort = null!;
        public ComHelper()
        {
            _serialPort = new SerialPort();
            _serialPort.DataBits = 8;            // 数据位  
            _serialPort.StopBits = StopBits.One;            // 停止位  
            _serialPort.Parity = Parity.None;            // 无奇偶校验位  
            _serialPort.DataReceived += ComDataReceived;
#if RS232
            serialPort.ReceivedBytesThreshold = 8;
#endif
        }

        public string Port
        { 
            get => _serialPort.PortName; 
            set
            {
                if(!_serialPort.IsOpen)
                    _serialPort.PortName = value; 
            } 
        }
        public int BaudRate
        {
            get => _serialPort.BaudRate;
            set
            {
                if (!_serialPort.IsOpen)
                    _serialPort.BaudRate = value; 
            }
        }
        public Action<string> DataReceived { get; set; } = null!;

        public static string[] GetComs() => SerialPort.GetPortNames(); 
        public bool Open()
        {
            if(_serialPort is null)
                return false;
            if (!string.IsNullOrEmpty(Port) || BaudRate < 0)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();
                _serialPort.Open();
                return true;
            }
            else throw new Exception("null portname or baudrate!");
        }
        public void Close() => _serialPort.Close();

        public async Task Write(byte[] buffer)
        {
            _serialPort.Write(buffer, 0, buffer.Length);
            await Task.Delay(100);
        }
        public void ComDataReceived(object o, SerialDataReceivedEventArgs e)
        {
            byte[] rxBuf = new byte[1000];
            int count = _serialPort.Read(rxBuf, 0, rxBuf.Length);
            string rawStr = Encoding.ASCII.GetString(rxBuf, 0, count);
            if(rawStr.Contains("Config"))
                DataReceived.Invoke(rawStr);
        }
    }
}