using SmartFlow.Shared.Models.Ede.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models.Ede.Codes
{
    public class CodeResponse
    {
        public ResultCode resultCode;
        public string resultMessage;
        public string lastUpdated;
        public IEnumerable<EdeCode> edeCodes;
        public IEnumerable<EdeCodeRelation> edeCodeRelations;
    }
}
