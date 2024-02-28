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
                int selectionFirstNumber = (int)cmbHost1B1.SelectedItem;
                cmbHost1B2.ItemsSource = ComboBoxDataLoader.LoadSecondNumberOptions(selectionFirstNumber);
                cmbHost1B2.SelectedIndex = 0;
                cmbHost1B3.ItemsSource = ComboBoxDataLoader.LoadThirdNumberOption();
                cmbHost1B3.SelectedIndex = 0;
                cmbHost1B4.ItemsSource = ComboBoxDataLoader.LoadFourthNumberOption();
                cmbHost1B4.SelectedIndex = 0;
                cmbHost1CIDR.ItemsSource = ComboBoxDataLoader.LoadCidrValues(selectionFirstNumber);
                cmbHost1CIDR.SelectedIndex = 0;

                RefreshHost1IpAddress();
            }         
        }

        private void CmbHost1B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshHost1IpAddress();
        }

        private void CmbHost1B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshHost1IpAddress();
        }

        private void CmbHost1B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshHost1IpAddress();
        }

        private void CmbHost1CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cidrValue = (int)cmbHost1CIDR.SelectedItem;
            string subnetBitSequence = ipCalculatorService.GetSubnetMask(cidrValue);

            RefreshHost1IpAddress();
            //txtHost1SubnetBinary.Text = subnetBitSequence;
            //txtHost1SubnetDD.Text = ipCalculatorService.FormatIpAddress(ipCalculatorService.SplitBitSequenceInBytes(subnetBitSequence));
            RefreshHost1SubnetInformation(subnetBitSequence);
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

                RefreshHost2IpAddress();
            }
        }

        private void CmbHost2B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            RefreshHost2IpAddress();
        }

        private void CmbHost2B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshHost2IpAddress();
        }

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshHost2IpAddress();
        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cidrValue = (int)cmbHost2CIDR.SelectedItem;
            RefreshHost2IpAddress();
            string subnetBitSequence = ipCalculatorService.GetSubnetMask(cidrValue);

            RefreshHost2SubnetInformation(subnetBitSequence);
        }

        void PopulateFirstComboBoxes()
        {       
            cmbHost1B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
            cmbHost2B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
        }

        List<int> GetHost1NumberValues()
        {
            List<int> host1DecimalValues = new List<int>
                {
                    (int)cmbHost1B1.SelectedItem,
                    (int)cmbHost1B2.SelectedItem,
                    (int)cmbHost1B3.SelectedItem,
                    (int)cmbHost1B4.SelectedItem
                };
            return host1DecimalValues;
        }

        List<int> GetHost2NumberValues()
        {
            List<int> host2DecimalValues = new List<int>
                {
                    (int)cmbHost2B1.SelectedItem,
                    (int)cmbHost2B2.SelectedItem,
                    (int)cmbHost2B3.SelectedItem,
                    (int)cmbHost2B4.SelectedItem
                };
            return host2DecimalValues;
        }

       void RefreshHost1SubnetInformation(string subnetBitSequence)
        {
            txtHost1SubnetBinary.Text = subnetBitSequence;
            txtHost1SubnetDD.Text = ipCalculatorService.FormatIpAddress(ipCalculatorService.SplitBitSequenceInBytes(subnetBitSequence));
        }

        void RefreshHost2SubnetInformation(string subnetBitSequence)
        {
            txtHost2SubnetBinary.Text = subnetBitSequence;
            txtHost2SubnetDD.Text = ipCalculatorService.FormatIpAddress(ipCalculatorService.SplitBitSequenceInBytes(subnetBitSequence));
        }


        void RefreshHost1IpAddress()
        {
            if (cmbHost1B1.SelectedItem != null && cmbHost1B2.SelectedItem != null && cmbHost1B3.SelectedItem != null && cmbHost1B4.SelectedItem != null)
            {
                txtHost1IPBinary.Text = ipCalculatorService.ConvertToByteSequence(GetHost1NumberValues());
                txtHost1IPDD.Text = ipCalculatorService.FormatIpAddress(GetHost1NumberValues());
            }
        }

        void RefreshHost2IpAddress()
        {
            if (cmbHost2B1.SelectedItem != null && cmbHost2B2.SelectedItem != null && cmbHost2B3.SelectedItem != null && cmbHost2B4.SelectedItem != null)
            {
                txtHost2IPBinary.Text = ipCalculatorService.ConvertToByteSequence(GetHost2NumberValues());
                txtHost2IPDD.Text = ipCalculatorService.FormatIpAddress(GetHost2NumberValues());
            }
        }
    }
}
