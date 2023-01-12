using Domain;
using System;
using Microsoft.Win32;
using System.ComponentModel;
using Domain.Com;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Controller.Models;

namespace Controller.ViewModels
{
    internal class ManWindowVM : INotifyPropertyChanged
    {
        # region 属性绑定
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand QuitAppCommand { get; set; } = null!;
        public RelayCommand SelectCfgPathCommand { get; set; } = null!;
        public RelayCommand ResetStateCommand { get; set; } = null!;
        public RelayCommand WriteInCommand { get; set; } = null!;
        public ManWindowVM()
        {
            QuitAppCommand = new RelayCommand() { ExecuteAction = QuitApp };
            SelectCfgPathCommand = new RelayCommand() { ExecuteAction = SelectCfgPath };
            ResetStateCommand = new RelayCommand() { ExecuteAction = ResetState };
            WriteInCommand = new RelayCommand() { ExecuteAction = WriteIn };
            _controller = new DeviceController();
            _controller.ReplyArrived += DealReply;
        }
        private ObservableCollection<string> _supportedComs = new ObservableCollection<string>();
        public ObservableCollection<string> SupportedComs
        {
            get
            {
                var coms = ComHelper.GetComs();
                if (_supportedComs.Count != coms.Length)
                {
                    _supportedComs.Clear();
                    foreach(var com in coms)
                        _supportedComs.Add(com);
                }
                return _supportedComs;
            }
        }
        private string? _selectedCom;
        public string? SelectedCom
        {
            get => _selectedCom;
            set
            {
                _selectedCom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCom"));
            }
        }

        private bool _ifWriteSn = true;
        public bool IfWriteSn
        {
            get => _ifWriteSn;
            set
            {
                _ifWriteSn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfWriteSn"));
            }
        }
        private string? _serialNumber;
        public string? SerialNumber
        { 
            get=> _serialNumber;
            set
            {
                _serialNumber = value;
                IfSnPass = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SerialNumber"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfSnPass"));
            } 
        }
        private bool _ifSnPass = false;
        public bool IfSnPass
        {
            get => _ifSnPass;
            set
            {
                _ifSnPass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfSnPass"));
            }
        }
        private bool _ifUseConfig;
        public bool IfUseConfig
        {
            get => _ifUseConfig;
            set
            {
                _ifUseConfig = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfUseConfig"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfNotUseConfig"));
            }
        }
        public bool IfNotUseConfig => !_ifUseConfig;
        private string? _configPath;
        public string? ConfigPath
        {
            get => _configPath;
            set
            {
                _configPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConfigPath"));
            }
        }
        private bool _ifWriteHost = true;
        public bool IfWriteHost
        {
            get => _ifWriteHost;
            set
            {
                _ifWriteHost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfWriteHost"));
            }
        }
        private string? _mqttServer;
        public string? MqttServer
        {
            get => _mqttServer;
            set
            {
                _mqttServer = value;
                IfHostPass = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MqttServer"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfHostPass"));
            }
        }
        private int? _mqttPort;
        public int? MqttPort
        {
            get => _mqttPort;
            set
            {
                _mqttPort = value;
                IfHostPass = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MqttPort"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfHostPass"));
            }
        }
        private bool _ifHostPass = false;
        public bool IfHostPass
        {
            get => _ifHostPass;
            set
            {
                _ifHostPass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfHostPass"));
            }
        }
        private bool _ifWriteUserName = true;
        public bool IfWriteUserName
        {
            get => _ifWriteUserName;
            set
            {
                _ifWriteUserName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfWriteUserName"));
            }
        }
        private string? _mqttUserName;
        public string? MqttUserName
        {
            get => _mqttUserName;
            set
            {
                IfUserNamePass = false;
                _mqttUserName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MqttUserName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfUserNamePass"));
            }
        }
        private bool _ifUserNamePass = false;
        public bool IfUserNamePass
        {
            get => _ifUserNamePass;
            set
            {
                _ifUserNamePass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfUserNamePass"));
            }
        }
        private bool _ifWritePassword = true;
        public bool IfWritePassword
        {
            get => _ifWritePassword;
            set
            {
                _ifWritePassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfWritePassword"));
            }
        }
        private string? _mqttPassword;
        public string? MqttPassword
        {
            get => _mqttPassword;
            set
            {
                IfPasswordPass = false;
                _mqttPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MqttPassword"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfPasswordPass"));
            }
        }
        private bool _ifPasswordPass = false;
        public bool IfPasswordPass
        {
            get => _ifPasswordPass;
            set
            {
                _ifPasswordPass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfPasswordPass"));
            }
        }
        private string? _helperInfo = MyVersion.Full;
        public string? HelperInfo
        {
            get => _helperInfo;
            set
            {
                _helperInfo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HelperInfo"));
            }
        }
        private ColorIndex _infocolor;
        public ColorIndex InfoColor
        {
            get => _infocolor;
            set
            {
                _infocolor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoColor"));
            }
        }
        #endregion

        private DeviceController _controller = null!;
        public void QuitApp(object o)
        {
            var window = o as System.Windows.Window;
            window?.Close();
        }
        public void SelectCfgPath(object o)
        {
            var dialog = new OpenFileDialog()
            {
                Title = "选择数据源文件",
                Filter = "配置文件(*.json,*.config)|*.json;*.config|所有文件|*.*",
                FileName = string.Empty,
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
                DefaultExt = "txt",
            };
            if(dialog?.ShowDialog()??false)
                ConfigPath = dialog.FileName;
        }
        public async void ResetState(object o)
        {
            try
            {
                _controller.Open(SelectedCom ?? throw new ArgumentException("no com was selected"), 115200);
                _controller?.Set(MessageType.Reset);
            }
            catch (Exception ex)
            {
                await ShowInfo(ex.Message, NotifyType.Exception);
            }
        }
        public async void WriteIn(object o)
        {
            try
            {
                await ShowInfo("正在执行写入...",NotifyType.State);
                _controller.Open(SelectedCom ?? throw new ArgumentException("no com was selected"), 115200);
                await ShowInfo($"串口开启成功", NotifyType.State);
                ControlMessage? controlMessage;
                if (IfUseConfig)
                {
                    controlMessage = JsonSerializer.Deserialize<ControlMessage>(ConfigPath ?? throw new ArgumentNullException("no config path was put in!"));
                }
                else
                {
                    controlMessage = new ControlMessage(
                        (string.IsNullOrEmpty(SerialNumber) || !IfWriteSn) ? null : SerialNumber,
                        (string.IsNullOrEmpty(MqttServer) || !IfWriteHost) ? null : SerialNumber,
                        (MqttPort is null || !IfWriteHost) ? null : MqttPort,
                        (string.IsNullOrEmpty(MqttUserName) || !IfWriteUserName) ? null : MqttUserName,
                        (string.IsNullOrEmpty(MqttServer) || !IfWritePassword) ? null : MqttPassword
                        );
                }
                if (controlMessage != null)
                {
                    await _controller.Set(MessageType.Reset);
                    await ShowInfo($"正在等待复位完成...", NotifyType.Instant);
                    await Task.Delay(5*1000);
                    await ShowInfo($"复位已完成...", NotifyType.State);
                    await _controller.SendMsg(controlMessage);
                    await _controller.Set(MessageType.Save);
                }
            }
            catch (Exception ex)
            {
                await ShowInfo(ex.Message, NotifyType.Exception);
            }
        }
        public async void DealReply(string reply)
        {
            if(!reply.Contains("OK"))
            {
                await ShowInfo(reply, NotifyType.Exception);
                return;
            }
            if(reply.Contains("SN"))
                IfSnPass = true;
            else if (reply.Contains("IP"))
                IfHostPass = true;
            else if (reply.Contains("UserName"))
                IfUserNamePass = true;
            else if (reply.Contains("Password"))
                IfPasswordPass = true;
            await ShowInfo(reply, NotifyType.Instant);
        }

        private async Task ShowInfo(string msg, NotifyType type)
        {
            InfoColor = type switch
            {
                NotifyType.Exception => ColorIndex.Red,
                NotifyType.State => ColorIndex.Green,
                NotifyType.Instant => ColorIndex.White,
                _ => throw new ArgumentException("unexcepted notify type")
            };
            HelperInfo = new NotificationBuilder()
                .WithHeader(SelectedCom ?? string.Empty)
                .WithTime(DateTime.Now)
                .WithType(type)
                .WithMsg(msg)
                .Build();
            if(type == NotifyType.State)
            {
                await Task.Delay(500);
                HelperInfo = string.Empty;
            }
            else if(type == NotifyType.Exception)
            {
                await Task.Delay(3 * 1000);
                HelperInfo = string.Empty;
            }
        }
    }

    public enum ColorIndex
    {
        White,
        Red,
        Green,
    }
}
