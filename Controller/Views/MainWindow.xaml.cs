﻿using Controller.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _mainVm = new MainWindowVM();

        public MainWindow()
        {
            this.DataContext = _mainVm;
            InitializeComponent();
        }

        public void WindowHeadDragMove(object sender, MouseButtonEventArgs e) => this.DragMove();
    }
}