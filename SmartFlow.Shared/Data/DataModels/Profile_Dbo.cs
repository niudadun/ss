using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFlow.Shared.Data.DataModels
{
    public class Profile_Dbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public List<Question_Dbo> Questions { get; set; }
        public string LastUpdatedDate { get; set; }
        public string CreatedDate { get; set; }
    }
}
