using IPCalculator.Core.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCalculator.Core.Entities
{
    public class Host
    {
        public List<int> UserInput { get; set; } = new List<int>();

        public int CidrValue { get; set; }
        public string IpAddressDD { get; set; }
        public string IpAddressBinary { get; set; }
        public string SubnetAddressDD { get; set; }
        public string SubnetAddressBinary { get; set; }
        public string NetworkAddressDD { get; set; }
        public string NetworkAddressBinary { get; set; }
        public string FirstHostAddressDD { get; set; }
        public string FirstHostAddressBinary { get; set; }
        public string LastHostAddressDD { get; set; }
        public string LastHostAddressBinary { get; set; }
        public string BroadCastAddressDD { get; set; }
        
        public string BroadCastAddressBinary { get; set; }

        private string FormatToDDNetworkAddress(List<int> numbers)
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

        private int ConvertBitsToDecimal(string bits)
        {
            return Convert.ToInt32(bits, 2);
        }

        private List<int> SplitBitSequenceInBytes(string bitSequence)
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

        private string ConvertToByteSequence(List<int> numbers)
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

    }
}
