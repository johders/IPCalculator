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
        public Host Host1 { get;}

        public Host Host2 { get;}

        public IpCalculatorService()
        {
            Host1 = new Host();
            Host2 = new Host();
        }

        public string FormatIpAddress(int num1, int num2, int num3, int num4)
        {
            return $"{num1}.{num2}.{num3}.{num4}";
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


    }
}
