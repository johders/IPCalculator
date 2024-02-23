using IPCalculator.Core.Entities;
using IPCalculator.Core.Service;
using System;
using System.Collections;
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
        //Host host1;
        //Host host2;
        public MainWindow()
        {
            InitializeComponent();
            ipCalculatorService = new IpCalculatorService();
            //host1 = new Host();
            //host2 = new Host();
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
                int selectionFirstNumber = (int)cmbHost1B1.SelectedItem;
                cmbHost1B2.ItemsSource = ComboBoxDataLoader.LoadSecondNumberOptions(selectionFirstNumber);
                cmbHost1B2.SelectedIndex = 0;
                cmbHost1B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
                cmbHost1B3.SelectedIndex = 0;
                cmbHost1B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
                cmbHost1B4.SelectedIndex = 0;
                cmbHost1CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
                cmbHost1CIDR.SelectedIndex = 0;
                ConvertToBitArray(selectionFirstNumber);
            }         
        }

        private void CmbHost1B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //if(cmbHost1B2.SelectedItem != null)
           // {
           //     cmbHost1B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
           // }
        }

        private void CmbHost1B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbHost1B3.SelectedItem != null)
            //{
            //    cmbHost1B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
            //}
        }

        private void CmbHost1B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbHost1B4.SelectedItem != null)
            //{
            //    int selectionFirstNumber = (int)cmbHost1B1.SelectedItem;
            //    cmbHost1CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
            //}
        }

        private void CmbHost1CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num1 = (int)cmbHost1B1.SelectedItem;
            int num2 = (int)cmbHost1B2.SelectedItem;
            int num3 = (int)cmbHost1B3.SelectedItem;
            int num4 = (int)cmbHost1B4.SelectedItem;
            if (cmbHost1CIDR.SelectedItem != null)
            {
                ipCalculatorService.Host1.IpAddress = ipCalculatorService.FormatIpAddress(num1, num2, num3, num4);
                txtHost1IPDD.Text = ipCalculatorService.Host1.IpAddress;
            }
        }

        private void CmbHost2B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHost2B1.SelectedItem != null)
            {
                int selectionFirstNumber = (int)cmbHost2B1.SelectedItem;
                cmbHost2B2.ItemsSource = ComboBoxDataLoader.LoadSecondNumberOptions(selectionFirstNumber);
                cmbHost2B2.SelectedIndex = 0;
                cmbHost2B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
                cmbHost2B3.SelectedIndex = 0;
                cmbHost2B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
                cmbHost2B4.SelectedIndex = 0;
                cmbHost2CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
                cmbHost2CIDR.SelectedIndex = 0;
            }
        }

        private void CmbHost2B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbHost2B2.SelectedItem != null)
            //{
            //    cmbHost2B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
            //}
        }

        private void CmbHost2B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbHost2B3.SelectedItem != null)
            //{
            //    cmbHost2B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
            //}
        }

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbHost2B4.SelectedItem != null)
            //{
            //    int selectionFirstNumber = (int)cmbHost2B1.SelectedItem;
            //    cmbHost2CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
            //}
        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num1 = (int)cmbHost2B1.SelectedItem;
            int num2 = (int)cmbHost2B2.SelectedItem;
            int num3 = (int)cmbHost2B3.SelectedItem;
            int num4 = (int)cmbHost2B4.SelectedItem;
            if (cmbHost2CIDR.SelectedItem != null)
            {
                ipCalculatorService.Host2.IpAddress = ipCalculatorService.FormatIpAddress(num1, num2, num3, num4);
                txtHost2IPDD.Text = ipCalculatorService.Host2.IpAddress;
            }
        }



        void PopulateFirstComboBoxes()
        {       
            cmbHost1B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
            cmbHost2B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
        }

        void ConvertToBitArray(int number)
        {
            StringBuilder sb = new StringBuilder();

            byte value = Convert.ToByte(number);
            BitArray b = new BitArray(new byte[] { value });

            for(int i = b.Count - 1; i >= 0; i--)
            {
                char c = b[i] ? '1' : '0';
                sb.Append(c);
            }

            txtHost1IPBinary.Text = sb.ToString();
        }
       
    }
}
