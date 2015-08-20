using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernAnimationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //panel.Children.Clear();
            mainRoot.Children.RemoveAt(0);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mwnd = new MainWindow();
            mwnd.Show();
        }
    }
}
