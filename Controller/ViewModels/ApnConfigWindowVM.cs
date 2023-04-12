using Controller.Utillities;
using Domain.Excel;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;

namespace Controller.ViewModels
{
    public class ApnConfigWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand LoadExcelCommand { get; set; } = null!;
        public RelayCommand SaveApnSettingsCommand { get; set; } = null!;
        public RelayCommand QuitApnConfigCommand { get; set; } = null!;

        public ApnConfigWindowVM()
        {
            LoadExcelCommand = new RelayCommand() { ExecuteAction = LoadExcel };
            SaveApnSettingsCommand = new RelayCommand() { ExecuteAction = SaveApnSettings };
            QuitApnConfigCommand = new RelayCommand() { ExecuteAction = QuitApnConfig };
        }

        private string? _apnIn;

        public string? ApnIn
        {
            get => _apnIn;
            set
            {
                _apnIn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ApnIn"));
            }
        }

        private string? _apnExtension;

        public string? ApnUsernameExtension
        {
            get => _apnExtension;
            set
            {
                _apnExtension = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ApnExtension"));
            }
        }

        private string? _apnPassword;

        public string? ApnPassword
        {
            get => _apnPassword;
            set
            {
                _apnPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ApnPassword"));
            }
        }

        private string? _apnConfigPath;

        public string? ApnConfigPath
        {
            get => _apnConfigPath;
            set
            {
                _apnConfigPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ApnConfigPath"));
            }
        }

        public void LoadExcel(object o)
        {
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "选择Excel文件",
                Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx|所有文件|*.*",
                FileName = string.Empty,
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
                DefaultExt = "txt",
            };
            if (dialog?.ShowDialog() ?? false)
                ApnConfigPath = dialog.FileName;
            using FileStream fsReader = new FileStream(ApnConfigPath ?? throw new Exception("未选中任何excel文件"), FileMode.Open, FileAccess.Read);
            var apns = ExcelLoader.LoadCardInfoFromExcel(fsReader, Path.GetExtension(ApnConfigPath), "Sheet0:客户名称,用户号码,ICCID,L_IMSI");
            foreach (var apn in apns)
                Common.Instance.ApnInfos.Add(apn);
        }

        public void SaveApnSettings(object o)
        {
            ApnIn = ApnIn ?? "public.vpdn";
            ApnUsernameExtension = ApnUsernameExtension ?? "@jxdermyy.vpdn.zj";
            ApnPassword = ApnPassword ?? "jx123456";
            Common.Instance.ApnSettings = new ApnSettings(ApnIn, ApnUsernameExtension, ApnPassword);
            (o as System.Windows.Window)?.Close();
            if (Common.Instance.ApnSettings is not null && ApnConfigPath is not null)
                Common.Instance.IfApnConfigured = true;
        }

        public void QuitApnConfig(object o)
            => (o as System.Windows.Window)?.Close();
    }
}