using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFlow.Shared.Data.DataModels
{
    public class Question_Dbo
    {
        public int Id { get; set; }
        public string QuestionKey { get; set; }
        public QuestionIdenfiers QuestionIdentifier { get; set; }
        public string AnswerText { get; set; }
        public string AnswerCode{ get; set; }
        public QuestionType Type { get; set; }
        public string LastUpdatedDate { get; set; }
        public string CreatedDate { get; set; }
    }
}
