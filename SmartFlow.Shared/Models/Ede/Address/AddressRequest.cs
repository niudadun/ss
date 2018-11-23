using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models.Ede.Address
{
    class AddressRequest
    {
        public string ipAddr;
        public string uniqueKey;
        public string serverToken;
        public string expirtyDate;
        public string cnonce;
        public string gmt;
        public string lastUpdated;
    }
}
