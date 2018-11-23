using SmartFlow.Shared.Data.DataModels;
using SmartFlow.Shared.DataTemplates;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFlow.Shared.Mappers.Db_ViewModel
{
    public static class ViewModelMapper
    {
        public static void MapDeclarationToViewModel(Declaration_Dbo declaration_Db, Declaration declaration)
        {
            declaration.Id = declaration_Db.Id;
            declaration.ProfileId = declaration_Db.ProfileId;
            declaration.DeNo = declaration_Db.DeNo;
            declaration.DeclarationType = declaration_Db.DeclarationType;
            declaration.CreatedDate = declaration_Db.CreatedDate;
            declaration.LastUpdatedDate = declaration_Db.LastUpdatedDate;
            MapQuestionsToViewModel(declaration_Db.Questions, declaration.Chapters[0].Questions);
            if (declaration_Db.Profile != null)
            {
                MapProfileToViewModel(declaration_Db.Profile, declaration.Profile);
            }
        }

        public static void MapProfileToViewModel(Profile_Dbo profile_Db, Profile profile)
        {
            profile.Id = profile_Db.Id;
            profile.Image = profile_Db.Image;
            profile.Name = profile_Db.Name;
            profile.CreatedDate = profile_Db.CreatedDate;
            profile.LastUpdatedDate = profile_Db.LastUpdatedDate;
            if (profile_Db.Questions != null)
            {
                MapQuestionsToViewModel(profile_Db.Questions, profile.Chapters[0].Questions);
            }
        }

        public static void MapProfilesToViewModel(List<Profile_Dbo> profile_Db_List, List<Profile> profile_List)
        {
            profile_Db_List.ForEach(i =>
            {
                var profile = ProfileTemplate.CreateProfile();
                MapProfileToViewModel(i, profile);
                profile_List.Add(profile);
            });
        }

        public static void MapDeclarationsToViewModel(List<Declaration_Dbo> declartion_Db_List, List<Declaration> declaration_List)
        {
            declartion_Db_List.ForEach(i =>
            {
                var declaration = DeclarationTemplate.CreateDeclaration();
                declaration.Profile = ProfileTemplate.CreateProfile();
                MapDeclarationToViewModel(i, declaration);
                declaration_List.Add(declaration);
            });
        }


        public static void MapQuestionsToViewModel(List<Question_Dbo> question_Db_List, List<Question> question_List)
        {
            foreach(var question_Db in question_Db_List)
            {
                foreach (var question in question_List.Where(i => i.QuestionIdentifier == question_Db.QuestionIdentifier))
                {
                    question.Id = question_Db.Id;
                    question.AnswerText = question_Db.AnswerText;
                    question.AnswerCode = question_Db.AnswerCode;
                    if (question.Type == QuestionType.DropDown)
                    {
                        var answer = question.Answers.Find(i => i.Code == question_Db.AnswerCode);
                        question.SelectedAnswerIndex = (answer != null) ? question.Answers.IndexOf(answer) : 0;
                    }
                }
            }
        }

        public static void MapProfilesBasicToViewModel(List<Profile_Dbo> profile_Db_List, List<Profile> profile_List)
        {
            profile_Db_List.ForEach(i =>
            {
                profile_List.Add(new Profile()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Image = i.Image,
                    LastUpdatedDate = i.LastUpdatedDate
                });
            });
        }

        public static void MapDeclarationsBasicToViewModel(List<Declaration_Dbo> declartion_Db_List, List<Declaration> declaration_List)
        {
            declartion_Db_List.ForEach(i =>
            {
                declaration_List.Add(new Declaration()
                {
                    Id = i.Id,
                    ProfileId = i.ProfileId,
                    DeclarationType = i.DeclarationType,
                    LastUpdatedDate = i.LastUpdatedDate
                });
            });
        }
       
    }
}
