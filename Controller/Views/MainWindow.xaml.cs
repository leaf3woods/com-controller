using Controller.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ManWindowVM _mainVm = new ManWindowVM();
            this.DataContext = _mainVm;
            InitializeComponent();
        }
        public void WindowHeadDragMove(object sender, MouseButtonEventArgs e) => this.DragMove();
    }
}
