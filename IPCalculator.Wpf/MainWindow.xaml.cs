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
using System.Xml;


namespace IPCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IpCalculatorService ipCalculatorService;
        string color = "white";
        bool isCompatible = false;

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
            uri = new Uri(directoryInfo.FullName + $"/Images/{color}.png");
            imgColor.Source = new BitmapImage(uri);
        }


        private void CmbHost1B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            GetAllHost1Info();
            DisplayHost1Information();

        }

        

        private void CmbHost1B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost1Info();
            DisplayHost1Information();
        }

        private void CmbHost1B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost1Info();
            DisplayHost1Information();
        }

        private void CmbHost1B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost1Info();
            DisplayHost1Information();
        }

        private void CmbHost1CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost1Info();
            DisplayHost1Information();
        }



        private void CmbHost2B1_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            GetAllHost2Info();
            DisplayHost2Information();

        }

        private void CmbHost2B2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost2Info();
            DisplayHost2Information();
        }

        private void CmbHost2B3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost2Info();
            DisplayHost2Information();
        }

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost2Info();
            DisplayHost2Information();
        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllHost2Info();
            DisplayHost2Information();
        }

        private void PopulateFirstComboBoxes()
        {
            cmbHost1B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
            cmbHost2B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
        }

        private void GetAllHost1Info()
        {
            if (cmbHost1B1.SelectedItem != null && cmbHost1B2.SelectedItem != null && cmbHost1B3.SelectedItem != null && cmbHost1B4.SelectedItem != null)
            {
                ipCalculatorService.Host1.UserInput = GetHost1UserInputValues();
                ipCalculatorService.Host1.CidrValue = GetHost1CidrValue();
                ipCalculatorService.RefreshHostIpAddress(ipCalculatorService.Host1);
                ipCalculatorService.RefreshHostSubnetInformation(ipCalculatorService.Host1);
                ipCalculatorService.GetHostNetworkAddress(ipCalculatorService.Host1, ipCalculatorService.Host1.IpAddressBinary, ipCalculatorService.Host1.CidrValue);
                ipCalculatorService.GetLastHostAddress(ipCalculatorService.Host1, ipCalculatorService.Host1.IpAddressBinary, ipCalculatorService.Host1.CidrValue);
                ipCalculatorService.GetFirstHostAddress(ipCalculatorService.Host1, ipCalculatorService.Host1.NetworkAddressBinary);
                ipCalculatorService.GetBroadcastAddress(ipCalculatorService.Host1, ipCalculatorService.Host1.LastHostAddressBinary);
                CheckForCompatibility();
            }
        }

        private void GetAllHost2Info()
        {
            if (cmbHost2B1.SelectedItem != null && cmbHost2B2.SelectedItem != null && cmbHost2B3.SelectedItem != null && cmbHost2B4.SelectedItem != null)
            {
                ipCalculatorService.Host2.UserInput = GetHost2UserInputValues();
                ipCalculatorService.Host2.CidrValue = GetHost2CidrValue();
                ipCalculatorService.RefreshHostIpAddress(ipCalculatorService.Host2);
                ipCalculatorService.RefreshHostSubnetInformation(ipCalculatorService.Host2);
                ipCalculatorService.GetHostNetworkAddress(ipCalculatorService.Host2, ipCalculatorService.Host2.IpAddressBinary, ipCalculatorService.Host2.CidrValue);
                ipCalculatorService.GetLastHostAddress(ipCalculatorService.Host2, ipCalculatorService.Host2.IpAddressBinary, ipCalculatorService.Host2.CidrValue);
                ipCalculatorService.GetFirstHostAddress(ipCalculatorService.Host2, ipCalculatorService.Host2.NetworkAddressBinary);
                ipCalculatorService.GetBroadcastAddress(ipCalculatorService.Host2, ipCalculatorService.Host2.LastHostAddressBinary);
                CheckForCompatibility();
            }
        }

        private void DisplayHost1Information()
        {
            txtHost1IPBinary.Text = ipCalculatorService.Host1.IpAddressBinary;
            txtHost1IPDD.Text = ipCalculatorService.Host1.IpAddressDD;

            txtHost1SubnetBinary.Text = ipCalculatorService.Host1.SubnetAddressBinary;
            txtHost1SubnetDD.Text = ipCalculatorService.Host1.SubnetAddressDD;

            txtHost1NetworkBinary.Text = ipCalculatorService.Host1.NetworkAddressBinary;
            txtHost1NetworkDD.Text = ipCalculatorService.Host1.NetworkAddressDD;

            txtHost1FirstHostBinary.Text = ipCalculatorService.Host1.FirstHostAddressBinary;
            txtHost1FirstHostDD.Text = ipCalculatorService.Host1.FirstHostAddressDD;

            txtHost1LastHostBinary.Text = ipCalculatorService.Host1.LastHostAddressBinary;
            txtHost1LastHostDD.Text = ipCalculatorService.Host1.LastHostAddressDD;

            txtHost1BroadcastBinary.Text = ipCalculatorService.Host1.BroadCastAddressBinary;
            txtHost1BroadcastDD.Text = ipCalculatorService.Host1.BroadCastAddressDD;
        }

        private void DisplayHost2Information()
        {
            txtHost2IPBinary.Text = ipCalculatorService.Host2.IpAddressBinary;
            txtHost2IPDD.Text = ipCalculatorService.Host2.IpAddressDD;

            txtHost2SubnetBinary.Text = ipCalculatorService.Host2.SubnetAddressBinary;
            txtHost2SubnetDD.Text = ipCalculatorService.Host2.SubnetAddressDD;

            txtHost2NetworkBinary.Text = ipCalculatorService.Host2.NetworkAddressBinary;
            txtHost2NetworkDD.Text = ipCalculatorService.Host2.NetworkAddressDD;

            txtHost2FirstHostBinary.Text = ipCalculatorService.Host2.FirstHostAddressBinary;
            txtHost2FirstHostDD.Text = ipCalculatorService.Host2.FirstHostAddressDD;

            txtHost2LastHostBinary.Text = ipCalculatorService.Host2.LastHostAddressBinary;
            txtHost2LastHostDD.Text = ipCalculatorService.Host2.LastHostAddressDD;

            txtHost2BroadcastBinary.Text = ipCalculatorService.Host2.BroadCastAddressBinary;
            txtHost2BroadcastDD.Text = ipCalculatorService.Host2.BroadCastAddressDD;
        }

        private void CheckForCompatibility()
        {
            if (!String.IsNullOrEmpty(txtHost2NetworkBinary.Text) && !String.IsNullOrEmpty(txtHost1NetworkBinary.Text))
            {
                isCompatible = ipCalculatorService.CheckIfSameNetwork();

                if (isCompatible)
                {
                    color = "green";
                }
                else color = "red";
                MakeColor();
            }
        }

        private int GetHost1CidrValue()
        {
            int cidrValue = 0;

            if (cmbHost1CIDR.SelectedItem != null)
            {
                cidrValue = (int)cmbHost1CIDR.SelectedItem;
            }
            return cidrValue;
        }

        private int GetHost2CidrValue()
        {
            int cidrValue = 0;

            if (cmbHost2CIDR.SelectedItem != null)
            {
                cidrValue = (int)cmbHost2CIDR.SelectedItem;
            }
            return cidrValue;
        }

        private List<int> GetHost1UserInputValues()
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

        private List<int> GetHost2UserInputValues()
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
    }
}
