using SmartFlow.Shared.Models.Ede.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models
{
    public class Declaration : BaseData
    {
        public int Id { get; set; }

        public string DeNo { get; set; }

        public DeclarationType DeclarationType { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }
    }

    public class BaseData
    {
        public List<Chapter> Chapters { get; set; }

        public string LastUpdatedDate { get; set; }

        public string CreatedDate { get; set; }

        public string Language { get; set; }
    }
}
