using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models
{
    public class ValidationConfig
    {
        public int Id { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime MinDate { get; set; }
        public bool ValidateSpecialCharacters { get; set; }
        public bool ValidateSpecialCharactersName { get; set; }
        public bool AllowOnlyNumeric { get; set; }
        public bool ValidateEmailAddress { get; set; }
        public bool ValidateContactNumber { get; set; }
        public bool IsRequired { get; set; }
    }
}
