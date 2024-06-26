﻿using IPCalculator.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCalculator.Core.Service
{
    public class IpCalculatorService
    {
        public Host Host1 { get; }
        public Host Host2 { get; }

        public IpCalculatorService()
        {
            Host1 = new Host();
            Host2 = new Host();
        }

        public void RefreshHostIpAddress(Host host)
        {
            host.IpAddressBinary = ConvertToByteSequence(host.UserInput);
            host.IpAddressDD = FormatToDDNetworkAddress(host.UserInput);
        }

        public void RefreshHostSubnetInformation(Host host)
        {
            int cidrValue = host.CidrValue;
            host.SubnetAddressBinary = GetSubnetMask(cidrValue);
            host.SubnetAddressDD = FormatToDDNetworkAddress(SplitBitSequenceInBytes(host.SubnetAddressBinary));
        }

        public void GetHostNetworkAddress(Host host, string ipAddress, int cidrValue)
        {
            
            string addressRange = ipAddress.Substring(0, cidrValue);
            string remainderAfterSplit = ipAddress.Substring(cidrValue, 32 - cidrValue);

            StringBuilder networkAddressRemainder = new StringBuilder();

            for (int i = 0; i < remainderAfterSplit.Length; i++)
            {
                networkAddressRemainder.Append("0");
            }

            host.NetworkAddressBinary = addressRange + networkAddressRemainder.ToString();
            host.NetworkAddressDD = FormatToDDNetworkAddress(SplitBitSequenceInBytes(host.NetworkAddressBinary));
        }

        public void GetLastHostAddress(Host host, string bitSequence, int cidrValue)
        {
            string addressRange = bitSequence.Substring(0, cidrValue);
            string remainderAfterSplit = bitSequence.Substring(cidrValue, 32 - cidrValue);

            StringBuilder replaceRemainderWithZeros = new StringBuilder();

            for (int i = 0; i < remainderAfterSplit.Length - 1; i++)
            {
                replaceRemainderWithZeros.Append("1");
            }

            string lastHostBinary = addressRange + replaceRemainderWithZeros.ToString() + "0";

            host.LastHostAddressBinary = lastHostBinary;
            host.LastHostAddressDD = FormatToDDNetworkAddress(SplitBitSequenceInBytes(host.LastHostAddressBinary));
        }

        public void GetBroadcastAddress(Host host, string lastHostNumber)
        {
            string firstPart = lastHostNumber.Substring(0, lastHostNumber.Length - 1);
            host.BroadCastAddressBinary = firstPart + "1";
            host.BroadCastAddressDD = FormatToDDNetworkAddress(SplitBitSequenceInBytes(host.BroadCastAddressBinary));
        }

        public void GetFirstHostAddress(Host host, string networkNumber)
        {
            string firstPart = networkNumber.Substring(0, networkNumber.Length - 1);
            host.FirstHostAddressBinary = firstPart + "1";
            host.FirstHostAddressDD = FormatToDDNetworkAddress(SplitBitSequenceInBytes(host.FirstHostAddressBinary));
        }

        public string FormatToDDNetworkAddress(List<int> numbers)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(numbers[0]);
            sb.Append(".");
            sb.Append(numbers[1]);
            sb.Append(".");
            sb.Append(numbers[2]);
            sb.Append(".");
            sb.Append(numbers[3]);
            return sb.ToString();
        }

        public string ConvertToByteSequence(List<int> numbers)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int number in numbers)
            {
                byte value = Convert.ToByte(number);
                BitArray b = new BitArray(new byte[] { value });

                for (int i = b.Count - 1; i >= 0; i--)
                {
                    char c = b[i] ? '1' : '0';
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public int ConvertBitsToDecimal(string bits)
        {
            return Convert.ToInt32(bits, 2);
        }

        public List<int> SplitBitSequenceInBytes(string bitSequence)
        {
            List<int> result = new List<int>();

            string decimal1 = bitSequence.Substring(0, 8);
            string decimal2 = bitSequence.Substring(8, 8);
            string decimal3 = bitSequence.Substring(16, 8);
            string decimal4 = bitSequence.Substring(24, 8);

            int number1 = ConvertBitsToDecimal(decimal1);
            int number2 = ConvertBitsToDecimal(decimal2);
            int number3 = ConvertBitsToDecimal(decimal3);
            int number4 = ConvertBitsToDecimal(decimal4);

            result.Add(number1);
            result.Add(number2);
            result.Add(number3);
            result.Add(number4);

            return result;
        }

        public string GetSubnetMask(int cidrValue)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cidrValue; i++)
            {
                sb.Append('1');
            }

            for (int i = 1; i <= (32 - cidrValue); i++)
            {
                sb.Append('0');
            }

            return sb.ToString();
        }

        public bool CheckIfSameNetwork()
        {
            if (Host1.NetworkAddressBinary == Host2.NetworkAddressBinary)
            {
                return true;
            }
            return false;
        }

    }
}
