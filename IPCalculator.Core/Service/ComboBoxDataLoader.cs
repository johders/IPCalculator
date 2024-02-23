using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCalculator.Core.Service
{
    public static class ComboBoxDataLoader
    {
        public static IEnumerable<int> AddFirstNumberOptions()
        {
            List <int>firstNumberOptions = new List<int>();

            firstNumberOptions.Add(10);
            firstNumberOptions.Add(172);
            firstNumberOptions.Add(192);

            return firstNumberOptions.AsReadOnly();
        }

        public static IEnumerable<int> LoadSecondNumberOptions(int firstNumberSelection)
        {
            List<int> secondNumberOptions = new List<int>();

            if (firstNumberSelection == 10)
            {
                for (int i = 0; i <= 255; i++)
                {
                    secondNumberOptions.Add(i);
                }
            }

            if (firstNumberSelection == 172)
            {
                for (int i = 16; i <= 31; i++)
                {
                    secondNumberOptions.Add(i);
                }
            }

            if (firstNumberSelection == 192)
            {
                secondNumberOptions.Add(168);
            }

            return secondNumberOptions.AsReadOnly();
        }

        public static IEnumerable<int> LoadThirdNumberOption()
        {
            List<int> thirdNumberOptions = new List<int>();


            for (int i = 0; i <= 255; i++)
            {
                thirdNumberOptions.Add(i);
            }

            return thirdNumberOptions.AsReadOnly();

        }

        public static IEnumerable<int> LoadFourthNumberOption()
        {
            List<int> fourthNumberOptions = new List<int>();

            for (int i = 1; i <= 244; i++)
            {
                fourthNumberOptions.Add(i);
            }

            return fourthNumberOptions.AsReadOnly();
        }

        public static IEnumerable<int> LoadCidrValues(int firstNumberSelection)
        {
            List<int> cidrValueOptions = new List<int>();

            if (firstNumberSelection == 10)
            {
                for (int i = 8; i <= 30; i++)
                {
                    cidrValueOptions.Add(i);
                }
            }

            if (firstNumberSelection == 172)
            {
                for (int i = 12; i <= 30; i++)
                {
                    cidrValueOptions.Add(i);
                }
            }

            if (firstNumberSelection == 192)
            {
                for (int i = 16; i <= 30; i++)
                {
                    cidrValueOptions.Add(i);
                }
            }

            return cidrValueOptions.AsReadOnly();
        }
    }
}
