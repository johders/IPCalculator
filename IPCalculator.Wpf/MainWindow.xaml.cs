using IPCalculator.Core.Service;
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
        IpCalculatorService ipCalculatorService;
        public MainWindow()
        {
            InitializeComponent();
            ipCalculatorService = new IpCalculatorService();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeColor();
            PopulateFirstComboBoxes();
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
            if(cmbHost1B1.SelectedItem != null)
            {
                cmbHost1B2.ItemsSource = null;
                cmbHost1B3.ItemsSource = null;
                cmbHost1B4.ItemsSource = null;
                cmbHost1CIDR.ItemsSource = null;
                int selectionFirstNumber = (int)cmbHost1B1.SelectedItem;
                cmbHost1B2.ItemsSource = ComboBoxDataLoader.LoadSecondNumberOptions(selectionFirstNumber);
            }         
        }

        private void CmbHost1B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(cmbHost1B2.SelectedItem != null)
            {
                cmbHost1B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
            }
        }

        private void CmbHost1B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost1B3.SelectedItem != null)
            {
                cmbHost1B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
            }
        }

        private void CmbHost1B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost1B4.SelectedItem != null)
            {
                int selectionFirstNumber = (int)cmbHost1B1.SelectedItem;
                cmbHost1CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
            }
        }

        private void CmbHost1CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CmbHost2B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost2B1.SelectedItem != null)
            {
                cmbHost2B2.ItemsSource = null;
                cmbHost2B3.ItemsSource = null;
                cmbHost2B4.ItemsSource = null;
                cmbHost2CIDR.ItemsSource = null;
                int selectionFirstNumber = (int)cmbHost2B1.SelectedItem;
                cmbHost2B2.ItemsSource = ComboBoxDataLoader.LoadSecondNumberOptions(selectionFirstNumber);
            }
        }

        private void CmbHost2B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost2B2.SelectedItem != null)
            {
                cmbHost2B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
            }
        }

        private void CmbHost2B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost2B3.SelectedItem != null)
            {
                cmbHost2B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
            }
        }

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost2B4.SelectedItem != null)
            {
                int selectionFirstNumber = (int)cmbHost2B1.SelectedItem;
                cmbHost2CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
            }
        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }



        void PopulateFirstComboBoxes()
        {       
            cmbHost1B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
            cmbHost2B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
        }

       
    }
}
