using SmartFlow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models
{
    public class Question
    {
        public int Id { get; set; }
        // QuestionKey is used when we have to submit the profile info with declaration api call. This is the key in the submitted json for the answer value
        public string QuestionKey{ get; set; }
        public string Text { get; set; }
        public QuestionIdenfiers QuestionIdentifier { get; set; }
        public int Order { get; set; }
        public QuestionType Type { get; set; }
        public int ChapterId { get; set; }
        public ValidationConfig Config { get; set; }
        public int SelectedAnswerIndex { get; set; }
        public List<Answer> Answers { get; set; }
        public string AnswerText { get; set; }
        public string AnswerCode { get; set; }
        public bool isMandatory { get; set; }
        public bool IsVisible { get; set; }
        public string LastUpdatedDate { get; set; }
        public string CreatedDate { get; set; }

        public Question()
        {
            AnswerText = string.Empty;
            IsVisible = true;
        }
    }

    public enum QuestionType
    {
        Text = 0,
        RadioButton =1,
        DropDown = 2,
        CheckBox = 3,
        Date = 5
    }
}
