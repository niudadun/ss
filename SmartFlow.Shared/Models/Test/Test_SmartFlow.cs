using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models.Test
{
    public class QuestionSheet
    {
        public int question_sheet_id;
        public string question_sheet_version;
        public string question_sheet_lastupd;
        public int? language_id;
        public IEnumerable<Chapter> chapters;
        public IEnumerable<Question> questions;
    }

    public class Chapter
    {
        public int chapter_id;
        public string chapter_text;
        public int chapter_order;
    }


    public class Question
    {
        public int question_id;
        public bool is_hidden;
        public int type_id;
        public int chapter_id;
        public string question_text;
        public int question_order;
        public bool is_required;
        public IEnumerable<Answer> answers;
        public Validation validation;
        public PreFill prefill;
    }

    public class Answer
    {
        public int answer_id;
        public string answer_text;
        public int answer_order;
        public IEnumerable<RelatedQuestion> related_questions;
    }

    public class RelatedQuestion
    {
        public int related_question_id;
        public int related_question_order;
    }

    public class Validation
    {
        public int min_length;
        public int max_length;
        public string max_date;
    }

    public class PreFill
    {
        public int credential_type_id;
        public string credential_field_names;
        public bool editable;
    }
}
