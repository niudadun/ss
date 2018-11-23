using SmartFlow.Shared.Models.Ede.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models.Ede.Configs
{
    public class ConfigResponse
    {
        public ResultCode resultCode;
        public string resultMessage;
        public string lastUpdated;
        public IEnumerable<EdeConfig> edeConfigs;
    }
}
