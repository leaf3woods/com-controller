using Controller.Utillities;
using Domain.Excel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Controller
{
    public class Common : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private static readonly object _lockobj = new object();

        private static Common? _instance = null!;

        public static Common Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (_lockobj)
                    {
                        if (_instance is null)
                            _instance = new Common();
                    }
                }
                return _instance;
            }
        }

        public ObservableCollection<ApnInfo> ApnInfos { get; set; } = new ObservableCollection<ApnInfo>();

        private bool _ifApnConfigured = false;

        public bool IfApnConfigured
        {
            get => _ifApnConfigured;
            set
            {
                _ifApnConfigured = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IfApnConfigured"));
            }
        }

        public string? ApnConfigPath;

        public string GetUserIdViaLIMSI(string limsi) =>
            (this.ApnInfos.SingleOrDefault(a => a.LIMSI == limsi) ??
            throw new ArgumentNullException("limsi is not exist")).UserId;

        public ApnSettings? ApnSettings { get; set; }
    }
}