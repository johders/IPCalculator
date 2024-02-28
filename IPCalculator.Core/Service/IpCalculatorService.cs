using IPCalculator.Core.Entities;
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
        public Host Host1 { get; set; }
        public Host Host2 { get; set; }

        public IpCalculatorService()
        {
            Host1 = new Host();
            Host2 = new Host();
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

        public string GetNetworkAddress(string bitSequence, int cidrValue)
        {
            string addressRange = bitSequence.Substring(0, cidrValue);
            string remainderAfterSplit = bitSequence.Substring(cidrValue, 32 - cidrValue);

            StringBuilder replaceRemainderWithZeros = new StringBuilder();

            for (int i = 0; i < remainderAfterSplit.Length; i++)
            {
                replaceRemainderWithZeros.Append("0");
            }
          
            string networkNumber = addressRange + replaceRemainderWithZeros.ToString();

            return networkNumber;
        }

        public string GetLargestHostAddress(string bitSequence, int cidrValue)
        {
            string addressRange = bitSequence.Substring(0, cidrValue);
            string remainderAfterSplit = bitSequence.Substring(cidrValue, 32 - cidrValue);

            StringBuilder replaceRemainderWithZeros = new StringBuilder();

            for (int i = 0; i < remainderAfterSplit.Length - 1; i++)
            {
                replaceRemainderWithZeros.Append("1");
            }

            string largestHost = addressRange + replaceRemainderWithZeros.ToString() + "0";

            return largestHost;
        }

        public string GetBroadcastAddress(string lastHostNumber)
        {
           
            string firstPart = lastHostNumber.Substring(0, lastHostNumber.Length - 1);
            string result = firstPart + "1";
            return result;
        }

        public string GetSmallestHostAddress(string networkNumber)
        {
           
            string firstPart = networkNumber.Substring(0, networkNumber.Length -1);
            string result = firstPart + "1";
            return result;
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

        public string GetSubnetMask (int cidrValue)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cidrValue; i++)
            {
                sb.Append('1');
            }

            for (int i = 1;i <= (32 - cidrValue); i++)
            {
                sb.Append('0');
            }

            return sb.ToString();
           
        }

    }
}
