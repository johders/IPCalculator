using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCalculator.Core.Service
{
    public class IpCalculatorService
    {

        public List<int> firstNumberOptions { get; set; }
        public List<int> secondNumberOptions { get; set; }
        public List<int> thirdNumberOptions { get; set; }
        public List<int> fourthNumberOptions { get; set; }


        public List<int> AddFirstNumberOptions()
        {
            firstNumberOptions = new List<int>();

            firstNumberOptions.Add(10);
            firstNumberOptions.Add(172);
            firstNumberOptions.Add(192);

            return firstNumberOptions;
        }

        public List<int> LoadSecondNumberOptions(int firstNumberSelection)
        {
            secondNumberOptions = new List<int>();

            if(firstNumberSelection == 10)
            {
                for(int i = 0; i <= 255; i++) 
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

            return secondNumberOptions;
        }

        public void LoadThirdAndFourthNumberOption()
        {
            thirdNumberOptions = new List<int>();
            fourthNumberOptions = new List<int>();

            for(int i = 0;i <= 255;i++) 
            {
                thirdNumberOptions.Add(i);
            }

            for (int i = 0; i <= 144; i++)
            {
                fourthNumberOptions.Add(i);
            }

        }
    }
}
