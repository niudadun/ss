using SmartFlow.Shared.Data.DataModels;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFlow.Shared.Mappers.Db_ViewModel
{
    public static class DBMapper
    {
        public static void MapDeclarationToDB(Declaration declaration, Declaration_Dbo declaration_Db, bool createOperation = false)
        {
            declaration_Db.ProfileId = declaration.ProfileId;
            declaration_Db.DeNo = declaration.DeNo;
            declaration_Db.DeclarationType = declaration.DeclarationType;
            declaration_Db.LastUpdatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
            if (createOperation)
            {
                declaration_Db.CreatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
                declaration_Db.Questions = new List<Question_Dbo>();
            }            
            MapQuestionListToDB(declaration.Chapters[0].Questions, declaration_Db.Questions, createOperation);
        }

        public static void MapProfileToDB(Profile profile, Profile_Dbo profile_Db, bool createOperation = false)
        {
            profile_Db.Image = profile.Image;
            profile_Db.Name = profile.Chapters[0].Questions.Find(i => i.QuestionIdentifier == Enums.QuestionIdenfiers.FULLNAME).AnswerText;
            profile_Db.LastUpdatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
            if (createOperation)
            {
                profile_Db.CreatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
                profile_Db.Questions = new List<Question_Dbo>();
            }            
            MapQuestionListToDB(profile.Chapters[0].Questions, profile_Db.Questions, createOperation);
        }

        public static void MapQuestionListToDB(List<Question> question_List, List<Question_Dbo> question_Db_List, bool createOperation = false)
        {
            if (createOperation)
            {
                if (question_List != null)
                {
                    foreach (var question in question_List)
                    {
                        var question_Db = new Question_Dbo();
                        MapQuestionToDB(question, question_Db, createOperation);
                        question_Db_List.Add(question_Db);
                    }
                }
            }
            else
            {
                if (question_Db_List != null)
                {
                    foreach (var question_Db in question_Db_List)
                    {
                        foreach (var question in question_List.Where(i => i.QuestionIdentifier == question_Db.QuestionIdentifier))
                        {
                            MapQuestionToDB(question, question_Db, createOperation);
                        }
                    }
                }
            }
        }


        public static void MapQuestionToDB(Question question, Question_Dbo question_Db, bool createOperation = false)
        {
            question_Db.Type = question.Type;
            question_Db.QuestionKey = question.QuestionKey;
            question_Db.QuestionIdentifier = question.QuestionIdentifier;
            if (question.Type == QuestionType.DropDown)
            {
                question_Db.AnswerText = question.Answers[question.SelectedAnswerIndex].Text;
                question_Db.AnswerCode = question.Answers[question.SelectedAnswerIndex].Code;
            }
            else
            {
                question_Db.AnswerText = question.AnswerText;
                question_Db.AnswerCode = question.AnswerCode;
            }
            question_Db.LastUpdatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
            if (createOperation)
            {
                question_Db.CreatedDate = DateTime.Now.ToString(Utils.CLIENT_DATE_FORMAT);
            }
        }


    }
}
