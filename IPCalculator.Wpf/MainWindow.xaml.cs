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
            if (cmbHost1B1.SelectedItem != null)
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

                ipCalculatorService.Host1.UserInput = GetHost1NumberValues();
                ipCalculatorService.RefreshHost1IpAddress();
                ipCalculatorService.RefreshHost1SubnetInformation();

                ShowHost1Data();
            }


        }

        void ShowHost1Data()
        {
            txtHost1IPBinary.Text = ipCalculatorService.Host1.IpAddressBinary;
            txtHost1IPDD.Text = ipCalculatorService.Host1.IpAddressDD;

            txtHost1SubnetBinary.Text = ipCalculatorService.Host1.SubnetAddressBinary;
            txtHost1SubnetDD.Text = ipCalculatorService.Host1.SubnetAddressDD;
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
            if(cmbHost1CIDR.SelectedItem != null)
            {
                ipCalculatorService.Host1.UserInput = GetHost1NumberValues();
                ipCalculatorService.RefreshHost1SubnetInformation();
                ShowHost1Data();
            }
                
            
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

        private void CmbHost2B4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CmbHost2CIDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }






        /// <summary>
        /// /////////////////////////////////////////////////////////////
        /// </summary>

        void PopulateFirstComboBoxes()
        {
            cmbHost1B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
            cmbHost2B1.ItemsSource = ComboBoxDataLoader.AddFirstNumberOptions();
        }

        void CheckCompatibility()
        {
            if (!String.IsNullOrEmpty(txtHost2NetworkBinary.Text) && !String.IsNullOrEmpty(txtHost1NetworkBinary.Text))
            {
                isCompatible = ipCalculatorService.CheckIfSameNetwork(txtHost1NetworkBinary.Text, txtHost2NetworkBinary.Text);

                if (isCompatible)
                {
                    color = "green";
                }
                else color = "red";
                MakeColor();
            }
        }

        int GetHost1CidrValue()
        {
            int cidrValue = 0;

            if (cmbHost1CIDR.SelectedItem != null)
            {
                cidrValue = (int)cmbHost1CIDR.SelectedItem;
            }

            return cidrValue;
        }

        int GetHost2CidrValue()
        {
            int cidrValue = 0;

            if (cmbHost2CIDR.SelectedItem != null)
            {
                cidrValue = (int)cmbHost2CIDR.SelectedItem;
            }

            return cidrValue;
        }

        List<int> GetHost1NumberValues()
        {
            List<int> host1DecimalValues = new List<int>
                {
                    (int)cmbHost1B1.SelectedItem,
                    (int)cmbHost1B2.SelectedItem,
                    (int)cmbHost1B3.SelectedItem,
                    (int)cmbHost1B4.SelectedItem,
                    (int)cmbHost1CIDR.SelectedItem
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

        void RefeshHost1NetworkInformation()
        {
            if (cmbHost1B1.SelectedItem != null && cmbHost1B2.SelectedItem != null && cmbHost1B3.SelectedItem != null && cmbHost1B4.SelectedItem != null)
            {
                string ipAddress = ipCalculatorService.ConvertToByteSequence(GetHost1NumberValues());
                int cidrValue = GetHost1CidrValue();
                string networkAddressBinary = ipCalculatorService.GetNetworkAddress(ipAddress, cidrValue);
                txtHost1NetworkBinary.Text = networkAddressBinary;
                txtHost1NetworkDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(networkAddressBinary));

                string firstHostBinary = ipCalculatorService.GetSmallestHostAddress(networkAddressBinary);
                txtHost1FirstHostBinary.Text = ipCalculatorService.GetSmallestHostAddress(firstHostBinary);
                txtHost1FirstHostDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(firstHostBinary));

                string lastHostBinary = ipCalculatorService.GetLargestHostAddress(networkAddressBinary, cidrValue);
                txtHost1LastHostBinary.Text = lastHostBinary;
                txtHost1LastHostDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(lastHostBinary));

                string broadcastBinary = ipCalculatorService.GetBroadcastAddress(lastHostBinary);
                txtHost1BroadcastBinary.Text = broadcastBinary;
                txtHost1BroadcastDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(broadcastBinary));

            }
        }

        void RefeshHost2NetworkInformation()
        {
            if (cmbHost2B1.SelectedItem != null && cmbHost2B2.SelectedItem != null && cmbHost2B3.SelectedItem != null && cmbHost2B4.SelectedItem != null)
            {
                string ipAddress = ipCalculatorService.ConvertToByteSequence(GetHost2NumberValues());
                int cidrValue = GetHost2CidrValue();
                string networkAddressBinary = ipCalculatorService.GetNetworkAddress(ipAddress, cidrValue);
                txtHost2NetworkBinary.Text = networkAddressBinary;
                txtHost2NetworkDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(networkAddressBinary));

                string firstHostBinary = ipCalculatorService.GetSmallestHostAddress(networkAddressBinary);
                txtHost2FirstHostBinary.Text = ipCalculatorService.GetSmallestHostAddress(firstHostBinary);
                txtHost2FirstHostDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(firstHostBinary));

                string lastHostBinary = ipCalculatorService.GetLargestHostAddress(networkAddressBinary, cidrValue);
                txtHost2LastHostBinary.Text = lastHostBinary;
                txtHost2LastHostDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(lastHostBinary));

                string broadcastBinary = ipCalculatorService.GetBroadcastAddress(lastHostBinary);
                txtHost2BroadcastBinary.Text = broadcastBinary;
                txtHost2BroadcastDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(broadcastBinary));

            }
        }


        void RefreshHost1SubnetInformation(string subnetBitSequence)
        {
            txtHost1SubnetBinary.Text = subnetBitSequence;
            txtHost1SubnetDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(subnetBitSequence));
        }

        void RefreshHost2SubnetInformation(string subnetBitSequence)
        {
            txtHost2SubnetBinary.Text = subnetBitSequence;
            txtHost2SubnetDD.Text = ipCalculatorService.FormatToDDNetworkAddress(ipCalculatorService.SplitBitSequenceInBytes(subnetBitSequence));
        }


        //void RefreshHost1IpAddress()
        //{
        //    if (cmbHost1B1.SelectedItem != null && cmbHost1B2.SelectedItem != null && cmbHost1B3.SelectedItem != null && cmbHost1B4.SelectedItem != null)
        //    {
        //        txtHost1IPBinary.Text = ipCalculatorService.ConvertToByteSequence(GetHost1NumberValues());
        //        txtHost1IPDD.Text = ipCalculatorService.FormatToDDNetworkAddress(GetHost1NumberValues());

        //    }
        //}

        void RefreshHost2IpAddress()
        {
            if (cmbHost2B1.SelectedItem != null && cmbHost2B2.SelectedItem != null && cmbHost2B3.SelectedItem != null && cmbHost2B4.SelectedItem != null)
            {
                txtHost2IPBinary.Text = ipCalculatorService.ConvertToByteSequence(GetHost2NumberValues());
                txtHost2IPDD.Text = ipCalculatorService.FormatToDDNetworkAddress(GetHost2NumberValues());
            }
        }
    }
}
