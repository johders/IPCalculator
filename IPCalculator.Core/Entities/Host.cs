﻿using IPCalculator.Core.Service;
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
    }
}
