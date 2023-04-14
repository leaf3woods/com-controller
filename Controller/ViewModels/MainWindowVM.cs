using AutoMapper;
using Controller.Models;
using Controller.Views;
using Domain;
using Domain.Com;
using Domain.Repos.Dtos;
using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region command binding

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand QuitAppCommand { get; set; } = null!;
        public RelayCommand SelectCfgPathCommand { get; set; } = null!;
        public RelayCommand ResetStateCommand { get; set; } = null!;
        public RelayCommand WriteInCommand { get; set; } = null!;
        public RelayCommand ApnConfigCommand { get; set; } = null!;

        public MainWindowVM()
        {
            QuitAppCommand = new RelayCommand() { ExecuteAction = QuitApp };
            SelectCfgPathCommand = new RelayCommand() { ExecuteAction = SelectCfgPath };
            ResetStateCommand = new RelayCommand() { ExecuteAction = ResetState };
            WriteInCommand = new RelayCommand() { ExecuteAction = WriteIn };
            ApnConfigCommand = new RelayCommand() { ExecuteAction = ApnConfig };
            _controller = new DeviceController();
            _controller.ReplyArrived += DealReply;
        }

        #endregion command binding

        public IDeviceRepo DeviceRepo { get; set; } = null!;

        private DeviceController _controller = null!;
        public IOperationLogRepo OperationLogRepo { get; set; } = null!;
        public IMapper Mapper { get; set; } = null!;
        public ApnConfigWindow ApnConfigWindow { get; set; } = null!;

        private static readonly string _yshelp = "13";

        #region property binding

        private ObservableCollection<string> _supportedComs = new ObservableCollection<string>();

        public ObservableCollection<string> SupportedComs
        {
            get
            {
                var coms = ComHelper.GetComs();
                if (_supportedComs.Count != coms.Length)
                {
                    _supportedComs.Clear();
                    foreach (var com in coms)
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

        public ObservableCollection<int> SupportedBaudrates { get => new ObservableCollection<int> { 9600, 19200, 115200 }; }

        private int? _selectedBaudrate;

        public int? SelectedBaudrate
        {
            get => _selectedBaudrate;
            set
            {
                _selectedBaudrate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedBaudrate"));
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
            get => _serialNumber;
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

        private string? _flashConfigPathPath;

        public string? FlashConfigPath
        {
            get => _flashConfigPathPath;
            set
            {
                _flashConfigPathPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FlashConfigPath"));
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

        private bool _ifWriteApn = true;

        public bool IfWriteApn
        {
            get => _ifWriteApn;
            set
            {
                _ifWriteApn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfWriteApn"));
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

        #endregion property binding

        #region command method

        public void QuitApp(object o)
        {
            var window = o as System.Windows.Window;
            window?.Close();
        }

        public async void SelectCfgPath(object o)
        {
            var defaultDirectory = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(FlashConfigPath))
            {
                if (FlashConfigPath.ToLower() == _yshelp)
                {
                    var mould = JsonSerializer.Serialize<ControlMessage>(new ControlMessage(Sn: null, MqttServer: null, MqttPort: null, MqttUserName: null, MqttPassword: null,
                        AccessPoint: null, ApUserId: null, ApPassword: null));
                    JsonDocument jsonDocument = JsonDocument.Parse(mould);
                    // 格式化输出
                    string formatedMould = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions()
                    {
                        // 整齐打印
                        WriteIndented = true,
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                    });
                    var buffer = Encoding.UTF8.GetBytes(formatedMould);
                    var fullPath = Path.Combine(defaultDirectory, "mould.json");
                    using (FileStream fsWrite = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fsWrite.Seek(0, SeekOrigin.Begin);
                        await fsWrite.WriteAsync(buffer, 0, buffer.Length);
                        await fsWrite.FlushAsync();
                    }
                }
                else
                    defaultDirectory = Directory.Exists(FlashConfigPath) ? FlashConfigPath : defaultDirectory;
            }
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = defaultDirectory,
                Title = "选择数据源文件",
                Filter = "配置文件(*.json,*.config)|*.json;*.config|所有文件|*.*",
                FileName = string.Empty,
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
                DefaultExt = "txt",
            };
            if (dialog?.ShowDialog() ?? false)
                FlashConfigPath = dialog.FileName;
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
                await ShowInfo("正在执行写入...", NotifyType.State);
                _controller.Open(SelectedCom ?? throw new ArgumentException("no com was selected"),
                    SelectedBaudrate ?? throw new ArgumentException("no baudrate was selected"));
                await ShowInfo($"串口开启成功", NotifyType.State);
                ControlMessage? controlMessage;
                var limsi = await _controller.GetLIMSI();
                var username = Common.Instance.GetUserIdViaLIMSI(limsi);
                var lastControl = Mapper.Map<ControlMessage>((await DeviceRepo.FindViaLimsi(limsi))?.Settings);
                if (IfUseConfig)
                    controlMessage = JsonSerializer.Deserialize<ControlMessage>(FlashConfigPath ?? throw new ArgumentNullException("no config path was put in!"));
                else
                {

                    controlMessage = new ControlMessage(
                        (string.IsNullOrEmpty(SerialNumber) || !IfWriteSn) ? null : SerialNumber,
                        (string.IsNullOrEmpty(MqttServer) || !IfWriteHost) ? null : MqttServer,
                        (MqttPort is null || !IfWriteHost) ? null : MqttPort,
                        (string.IsNullOrEmpty(MqttUserName) || !IfWriteUserName) ? null : MqttUserName,
                        (string.IsNullOrEmpty(MqttPassword) || !IfWritePassword) ? null : MqttPassword,
                        (string.IsNullOrEmpty(Common.Instance.ApnSettings?.AccessPoint) || !IfWriteApn) ? null : Common.Instance.ApnSettings?.AccessPoint,
                        (string.IsNullOrEmpty(Common.Instance.ApnSettings?.UsernameExtension) || !IfWriteApn) ? null : username + Common.Instance.ApnSettings?.UsernameExtension,
                        (string.IsNullOrEmpty(Common.Instance.ApnSettings?.ApPassword) || !IfWriteApn) ? null : Common.Instance.ApnSettings?.ApPassword
                        );
                    lastControl = Mapper.Map<ControlMessage>(controlMessage);
                }
                if (lastControl != null)
                {
                    await _controller.Set(MessageType.Reset);
                    await ShowInfo($"正在等待复位完成...", NotifyType.Instant);
                    await Task.Delay(5 * 1000);
                    await ShowInfo($"复位完成,烧入配置...", NotifyType.State);
                    await _controller.SendMsg(lastControl);
                    await _controller.Set(MessageType.Save);
                    await ShowInfo($"烧入完成！", NotifyType.State);
                    await AddOperationRecord(new DeviceCreateDto()
                    {
                        Uri = SerialNumber ?? "",
                        Limsi = limsi,
                        Settings = Mapper.Map<Setting>(lastControl)
                    }, new OperationLogCreateDto
                    {
                        OperationTime = DateTime.Now,
                        FlashInState = true,
                    });
                }
            }
            catch (Exception ex)
            {
                await ShowInfo(ex.Message, NotifyType.Exception);
            }
        }

        public void ApnConfig(object o)
        {
            Common.Instance.IfApnConfigured = false;
            ApnConfigWindow.ShowDialog();
            ApnConfigWindow.Activate();
        }

        #endregion command method

        #region repo method

        private async Task AddOperationRecord(DeviceCreateDto deviceDto, OperationLogCreateDto operationDto)
        {
            var device = await DeviceRepo.FindViaLimsi(deviceDto.Limsi);
            if (device is null)
            {
                var afterCreate = await DeviceRepo.Create(deviceDto);
                operationDto.DeviceId = afterCreate!.Id;
            }
            else
            {
                if (!string.IsNullOrEmpty(deviceDto.Uri))
                    await DeviceRepo.Update(new DeviceUpdateDto() { Uri = deviceDto.Uri, Id = device.Id, Settings = deviceDto.Settings});
                var lastLog = (await OperationLogRepo.FindViaLimsi(deviceDto.Limsi)).MaxBy(o => o.OperationTime);
                operationDto.DeviceId = device.Id;
                var time = lastLog is null ? "null" : DateOnly.FromDateTime(lastLog.OperationTime).ToString();
                await ShowInfo("上次烧入时间:" + time, NotifyType.Instant);
            }
            await OperationLogRepo.Create(operationDto);
        }

        #endregion repo method

        public async void DealReply(string reply)
        {
            if (!reply.Contains("OK"))
            {
                await ShowInfo(reply, NotifyType.Exception);
                return;
            }
            if (reply.Contains("SN"))
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
            if (type == NotifyType.State)
            {
                await Task.Delay(500);
                HelperInfo = string.Empty;
            }
            else if (type == NotifyType.Exception)
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