using Controller.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Controller.Views
{
    /// <summary>
    /// ApnConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ApnConfigWindow : Window
    {
        private ApnConfigWindowVM _apnVM = new ApnConfigWindowVM();

        public ApnConfigWindow()
        {
            this.DataContext = _apnVM;
            InitializeComponent();
        }

        public void WindowHeadDragMove(object sender, MouseButtonEventArgs e) => this.DragMove();
    }
}