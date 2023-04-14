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
        public ApnConfigWindowVM ApnVM { get; set; } = null!;

        public ApnConfigWindow(ApnConfigWindowVM apnVM)
        {
            ApnVM = apnVM;
            this.DataContext = ApnVM;
            InitializeComponent();
        }

        public void WindowHeadDragMove(object sender, MouseButtonEventArgs e) => this.DragMove();
    }
}