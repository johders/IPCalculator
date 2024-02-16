using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace IPCalculator.Wpf
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void MakeColor()
        {
            string pad = Environment.CurrentDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(pad);
            directoryInfo = new DirectoryInfo(directoryInfo.Parent.Parent.Parent.FullName);
            Uri uri;
            uri = new Uri(directoryInfo.FullName + "/Images/white.png");
            imgColor.Source = new BitmapImage(uri);
        }
        private void CmbHost1B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost1B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost1B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost1B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost1CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
