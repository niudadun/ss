using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartFlow.Shared.DataTemplates
{
    /// <summary>
    /// This class is used to save template for the Profile questions which will be populated in the input view.
    /// </summary>
    class ProfileTemplate
    {
        private static string TAG = "ProfileTemplate";

        /// <summary>
        /// Function to create Profile template
        /// </summary>
        /// <returns></returns>
        public static Profile CreateProfile()
        {
            var proifleTemplate = new Profile()
            {
                CreatedDate = DateTime.Now.ToString(Shared.Helpers.Utils.CLIENT_DATE_FORMAT),
                LastUpdatedDate = DateTime.Now.ToString(Shared.Helpers.Utils.CLIENT_DATE_FORMAT),
                Chapters = CreateProfileChapters(),
                Language = "English"
            };

            return proifleTemplate;
        }

        /// <summary>
        /// Method to add chapters/questions/allowed options etc to the profile template.
        /// This template is then used to add the views in the input screen.
        /// Since we already know the questions in the profile screen, hence we can hardcode this template here.
        /// Question text will be picked from the Api label mapping.
        /// </summary>
        /// <returns></returns>
        public static List<Chapter> CreateProfileChapters()
        {
            var chapters = new List<Chapter>()
            {
                new Chapter()
                {
                     Order = 1,
                     Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PARTICULARS),
                     ChapterIdentifier = ChapterIdentifiers.Particulars,
                     ChapterClassId = ChapterIdentifiers.Particulars.ToString(),
                     Questions = new List<Question>()
                     {
                         new Question()
                        {
                             QuestionKey = "name",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.FULLNAME),
                             Order =1,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.FULLNAME,
                             isMandatory = true,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 66,
                                 ValidateSpecialCharactersName = true,
                                 IsRequired = true,
                             }
                        },
                         new Question()
                        {
                             QuestionKey = "sex",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.SEX),
                             Order =2,
                             Type = QuestionType.DropDown,
                             QuestionIdentifier = QuestionIdenfiers.SEX,
                             isMandatory = true,
                             Answers = new List<Answer>()
                             {
                                 new Answer()
                                 {
                                     Order =1,
                                     Code = "ST",
                                     Text =  Helpers.L10n.GetMappedString(TextMapping.SELECT_TEXT),
                                 },
                                 new Answer()
                                 {
                                     Order =2,
                                     Code = "M",
                                     Text = Helpers.L10n.GetDropDowneMappedString(DropDownData.Male)
                                 },
                                 new Answer()
                                 {
                                     Order =3,
                                     Code = "F",
                                     Text = Helpers.L10n.GetDropDowneMappedString(DropDownData.Female),
                                 }

                             },
                              Config = new ValidationConfig()
                              {
                                  IsRequired = true
                              }
                        },
                         new Question()
                        {
                             QuestionKey = "dateOfBirth",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.DATE_OF_BIRTH),
                             Order =3,
                             Type = QuestionType.Date,
                             QuestionIdentifier = QuestionIdenfiers.DATE_OF_BIRTH,
                             AnswerText = DateTime.Now.ToString(Shared.Helpers.Utils.DATE_FORMAT),
                             Config = new ValidationConfig()
                             {
                                 MaxDate = Utils.GetDateInFormat(DateTime.Now),
                                 MinDate = Utils.GetDateInFormat(DateTime.Now.AddYears(-100)),
                                  IsRequired = true
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "ctryBirth",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.COUNTRY_OF_BIRTH),
                             Order =8,
                             Type = QuestionType.DropDown,
                             QuestionIdentifier = QuestionIdenfiers.COUNTRY_OF_BIRTH,
                             Answers = StaticData.GetCountryList(QuestionIdenfiers.COUNTRY_OF_BIRTH),
                             isMandatory = true,
                              Config = new ValidationConfig()
                              {
                                  IsRequired = true
                              }
                        },
                        new Question()
                        {
                            QuestionKey = "natcd",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.NATIONALITY),
                             Order =9,
                             Type = QuestionType.DropDown,
                             QuestionIdentifier = QuestionIdenfiers.NATIONALITY,
                             Answers = StaticData.GetCountryList(QuestionIdenfiers.NATIONALITY),
                             isMandatory = true,
                              Config = new ValidationConfig()
                              {
                                  IsRequired = true
                              }
                        },
                        new Question()
                        {
                            QuestionKey = "mid",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.IDENTITY_CARD_NUMBER),
                             Order =10,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.IDENTITY_CARD_NUMBER,
                             IsVisible = false,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 12,
                                 IsRequired = true,
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "tdno",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PASSPORT_NUMBER),
                             Order =11,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.PASSPORT_NUMBER,
                             isMandatory = true,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 20,
                                 IsRequired = true,
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "tdExpiryDate",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PASSPORT_EXPIRY),
                             Order =12,
                             Type = QuestionType.Date,
                             QuestionIdentifier = QuestionIdenfiers.PASSPORT_EXPIRY,
                             AnswerText = DateTime.Now.ToString(Shared.Helpers.Utils.DATE_FORMAT),
                             Config = new ValidationConfig()
                             {
                                 MinDate = Utils.GetDateInFormat(DateTime.Now),
                                 MaxDate = Utils.GetDateInFormat(DateTime.Now.AddYears(10)),
                                  IsRequired = true
                             }
                        },
                        new Question()
                        { 
                            // No place of Residence required in Profile as per Design
                            QuestionKey = "placeResidence",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PLACE_OF_RESIDENCE),
                             Order =4,
                             IsVisible = false,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.PLACE_OF_RESIDENCE,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 30,
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "residenceCtry",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.COUNTRY_OF_RESIDENCE),
                             Order =5,
                             Type = QuestionType.DropDown,
                             QuestionIdentifier = QuestionIdenfiers.COUNTRY_OF_RESIDENCE,
                             Answers = StaticData.GetCountryList(QuestionIdenfiers.COUNTRY_OF_RESIDENCE),
                             isMandatory = true,
                              Config = new ValidationConfig()
                              {
                                  IsRequired = true
                              }
                        },
                        new Question()
                        {
                            QuestionKey = "residenceState",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.STATE_OF_RESIDENCE),
                             Order =6,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.STATE_OF_RESIDENCE,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 5,
                                 IsRequired = true
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "residenceCity",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.CITY_OF_RESIDENCE),
                             Order =7,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.CITY_OF_RESIDENCE,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 5,
                                 IsRequired = true
                             }
                        },
                        new Question()
                        {
                            QuestionKey = "contactNoCtry",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PHONE_NUMBER_COUNTRY),
                             Order =9,
                             Type = QuestionType.DropDown,
                             QuestionIdentifier = QuestionIdenfiers.PHONE_NUMBER_COUNTRY,
                             Answers = StaticData.GetCountryList(QuestionIdenfiers.PHONE_NUMBER_COUNTRY),
                             isMandatory = true,
                              Config = new ValidationConfig()
                              {
                                  IsRequired = true
                              }
                        },
                        new Question()
                        {
                            QuestionKey = "contactNo",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.PHONE_NUMBER),
                             Order =13,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.PHONE_NUMBER,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 12,
                                 ValidateContactNumber = true,
                                 IsRequired = true
                             }
                        },
                        new Question()
                        {
                             QuestionKey = "emailAddr",
                             Text = Helpers.L10n.GetQuestionIdentifierMappedString(QuestionIdenfiers.EMAIL),
                             Order =14,
                             Type = QuestionType.Text,
                             QuestionIdentifier = QuestionIdenfiers.EMAIL,
                             Config = new ValidationConfig()
                             {
                                 MaxLength = 100,
                                 ValidateEmailAddress = true,
                             }
                        }
                     }
                },
            };

            return chapters;
        }
    }

    /// <summary>
    /// Static Data information
    /// </summary>
    public class StaticData
    {
        /// <summary>
        /// Get the list of countries for selection
        /// </summary>
        /// <returns></returns>
        public static List<Answer> GetCountryList(QuestionIdenfiers QuestionIdentifier)
        {
            var list = GetCountryListString();
            var answersList = new List<Answer>
            {
                new Answer()
                {
                    Code = "ST",
                    Text =  Helpers.L10n.GetMappedString(TextMapping.SELECT_TEXT)
                }
            };

            if (QuestionIdentifier == QuestionIdenfiers.NATIONALITY)
            {
                int Index = list.FindIndex(i => i.Text == "Malaysia");
                if (Index >= 0)
                {
                    list[Index].ConditionalQuestion = new List<ConditionalQuestion>() { new ConditionalQuestion(QuestionIdenfiers.IDENTITY_CARD_NUMBER) };
                }
            }
            answersList.InsertRange(1, list);

            return answersList;
        }

        /// <summary>
        /// Countries string array
        /// </summary>
        /// <returns></returns>
        public static List<Answer> GetCountryListString()
        {
            return new List<Answer>()
                             {
                                    new Answer("Afghanistan","AFG"),
                                    new Answer("Albania","ALB"),
                                    new Answer("Algeria","ALG"),
                                    new Answer("American Samoa","AME"),
                                    new Answer("Andorra","Ando"),
                                    new Answer("Angola","Ang"),
                                    new Answer("Anguilla","Anug"),
                                    new Answer("Antarctica","Ant"),
                                    new Answer("Antigua and Barbuda","Anti"),
                                    new Answer("Argentina","Arg"),
                                    new Answer("Armenia","Arm"),
                                    new Answer("Aruba","Aru"),
                                    new Answer("Australia","Aus"),
                                    new Answer("Austria","Aust"),
                                    new Answer("Azerbaijan","Aze"),
                                    new Answer("Bahamas","Bah"),
                                    new Answer("Bahrain","Bahr"),
                                    new Answer("Bangladesh","Ban"),
                                    new Answer("Barbados","Bar"),
                                    new Answer("Belarus","Bel"),
                                    new Answer("Belgium","Belg"),
                                    new Answer("Belize","Beli"),
                                    new Answer("Benin","Ben"),
                                    new Answer("Bermuda","Ber"),
                                    new Answer("Bhutan","Bhu"),
                                    new Answer("Bolivia","Bol"),
                                    new Answer("Bosnia and Herzegovina","Bos"),
                                    new Answer("Botswana","Bot"),
                                    new Answer("Bouvet Island","Bou"),
                                    new Answer("Brazil","Bra"),
                                    new Answer("British Indian Ocean Territory","Bri"),
                                    new Answer("Brunei Darussalam","Bru"),
                                    new Answer("Bulgaria","Bul"),
                                    new Answer("Burkina Faso","Bur"),
                                    new Answer("Burundi","Buru"),
                                    new Answer("Cambodia","Cam"),
                                    new Answer("Cameroon","Came"),
                                    new Answer("Canada","Can"),
                                    new Answer("Cape Verde","Cap"),
                                    new Answer("Cayman Islands","Cay"),
                                    new Answer("Central African Republic","Cen"),
                                    new Answer("Chad","Cha"),
                                    new Answer("Chile","Chi"),
                                    new Answer("China","Chin"),
                                    new Answer("Christmas Island","Chr"),
                                    new Answer("Cocos (Keeling) Islands","Coc"),
                                    new Answer("Colombia","Col"),
                                    new Answer("Comoros","Com"),
                                    new Answer("Congo","Con"),
                                    new Answer("Congo, the Democratic Republic of the","Cong"),
                                    new Answer("Cook Islands","Coo"),
                                    new Answer("Costa Rica","Cos"),
                                    new Answer("Cote D'Ivoire","Cot"),
                                    new Answer("Croatia","Cro"),
                                    new Answer("Cuba","Cub"),
                                    new Answer("Cyprus","Cyp"),
                                    new Answer("Czech Republic","Cze"),
                                    new Answer("Denmark","Den"),
                                    new Answer("Djibouti","Dji"),
                                    new Answer("Dominica","Dom"),
                                    new Answer("Dominican Republic","Domi"),
                                    new Answer("Ecuador","Ecu"),
                                    new Answer("Egypt","Egy"),
                                    new Answer("El Salvador","ElS"),
                                    new Answer("Equatorial Guinea","Equ"),
                                    new Answer("Eritrea","Eri"),
                                    new Answer("Estonia","Est"),
                                    new Answer("Ethiopia","Eth"),
                                    new Answer("Falkland Islands (Malvinas)","Fal"),
                                    new Answer("Faroe Islands","Far"),
                                    new Answer("Fiji","Fij"),
                                    new Answer("Finland","Fin"),
                                    new Answer("France","Fra"),
                                    new Answer("French Guiana","Fre"),
                                    new Answer("French Polynesia","FreP"),
                                    new Answer("French Southern Territories","FreS"),
                                    new Answer("Gabon","Gab"),
                                    new Answer("Gambia","Gam"),
                                    new Answer("Georgia","Geo"),
                                    new Answer("Germany","Ger"),
                                    new Answer("Ghana","Gha"),
                                    new Answer("Gibraltar","Gib"),
                                    new Answer("Greece","Gre"),
                                    new Answer("Greenland","Gree"),
                                    new Answer("Grenada","Gren"),
                                    new Answer("Guadeloupe","Gua"),
                                    new Answer("Guam","Guam"),
                                    new Answer("Guatemala","Guat"),
                                    new Answer("Guinea","Gui"),
                                    new Answer("Guinea-Bissau","Guin"),
                                    new Answer("Guyana","Guy"),
                                    new Answer("Haiti","Hai"),
                                    new Answer("Heard Island and Mcdonald Islands","Hea"),
                                    new Answer("Holy See (Vatican City State)","Hol"),
                                    new Answer("Honduras","Hon"),
                                    new Answer("Hong Kong","Hong"),
                                    new Answer("Hungary","Hun"),
                                    new Answer("Iceland","Ice"),
                                    new Answer("India","Ind"),
                                    new Answer("Indonesia","Indo"),
                                    new Answer("Iran, Islamic Republic of","Ira"),
                                    new Answer("Iraq","Iraq"),
                                    new Answer("Ireland","Ire"),
                                    new Answer("Israel","Isr"),
                                    new Answer("Italy","Ita"),
                                    new Answer("Jamaica","Jam"),
                                    new Answer("Japan","Jap"),
                                    new Answer("Jordan","Jor"),
                                    new Answer("Kazakhstan","Kaz"),
                                    new Answer("Kenya","Ken"),
                                    new Answer("Kiribati","Kir"),
                                    new Answer("Korea, Democratic People's Republic of","Kor"),
                                    new Answer("Korea, Republic of","Kore"),
                                    new Answer("Kuwait","Kuw"),
                                    new Answer("Kyrgyzstan","Kyr"),
                                    new Answer("Lao People's Democratic Republic","Lao"),
                                    new Answer("Latvia","Lat"),
                                    new Answer("Lebanon","Leb"),
                                    new Answer("Lesotho","Les"),
                                    new Answer("Liberia","Lib"),
                                    new Answer("Libyan Arab Jamahiriya","Liby"),
                                    new Answer("Liechtenstein","Lie"),
                                    new Answer("Lithuania","Lit"),
                                    new Answer("Luxembourg","Lux"),
                                    new Answer("Macao","Mac"),
                                    new Answer("Macedonia, the Former Yugoslav Republic of","Mace"),
                                    new Answer("Madagascar","Mad"),
                                    new Answer("Malawi","Mal"),
                                    new Answer("Malaysia","Mala"),
                                    new Answer("Maldives","Mald"),
                                    new Answer("Mali","Mali"),
                                    new Answer("Malta","Malt"),
                                    new Answer("Marshall Islands","Mar"),
                                    new Answer("Martinique","Mart"),
                                    new Answer("Mauritania","Mau"),
                                    new Answer("Mauritius","Maur"),
                                    new Answer("Mayotte","May"),
                                    new Answer("Mexico","Mex"),
                                    new Answer("Micronesia, Federated States of","Mic"),
                                    new Answer("Moldova, Republic of","Mol"),
                                    new Answer("Monaco","Mon"),
                                    new Answer("Mongolia","Mong"),
                                    new Answer("Montserrat","Mont"),
                                    new Answer("Morocco","Mor"),
                                    new Answer("Mozambique","Moz"),
                                    new Answer("Myanmar","Mya"),
                                    new Answer("Namibia","Nam"),
                                    new Answer("Nauru","Nau"),
                                    new Answer("Nepal","Nep"),
                                    new Answer("Netherlands","Net"),
                                    new Answer("Netherlands Antilles","Net"),
                                    new Answer("New Caledonia","New"),
                                    new Answer("New Zealand","NewZ"),
                                    new Answer("Nicaragua","Nic"),
                                    new Answer("Niger","Nig"),
                                    new Answer("Nigeria","Nige"),
                                    new Answer("Niue","Niu"),
                                    new Answer("Norfolk Island","Nor"),
                                    new Answer("Northern Mariana Islands","Nort"),
                                    new Answer("Norway","Norw"),
                                    new Answer("Oman","Oma"),
                                    new Answer("Pakistan","Pak"),
                                    new Answer("Palau","Pal"),
                                    new Answer("Palestinian Territory, Occupied","Pale"),
                                    new Answer("Panama","Pan"),
                                    new Answer("Papua New Guinea","Pap"),
                                    new Answer("Paraguay","Par"),
                                    new Answer("Peru","Per"),
                                    new Answer("Philippines","Phi"),
                                    new Answer("Pitcairn","Pit"),
                                    new Answer("Poland","Pol"),
                                    new Answer("Portugal","Por"),
                                    new Answer("Puerto Rico","Pue"),
                                    new Answer("Qatar","Qat"),
                                    new Answer("Reunion","Reu"),
                                    new Answer("Romania","Rom"),
                                    new Answer("Russian Federation","Rus"),
                                    new Answer("Rwanda","Rwa"),
                                    new Answer("Saint Helena","Sai"),
                                    new Answer("Saint Kitts and Nevis","Sain"),
                                    new Answer("Saint Lucia","SaiL"),
                                    new Answer("Saint Pierre and Miquelon","SaiP"),
                                    new Answer("Saint Vincent and the Grenadines","SaiV"),
                                    new Answer("Samoa","Sam"),
                                    new Answer("San Marino","San"),
                                    new Answer("Sao Tome and Principe","Sao"),
                                    new Answer("Saudi Arabia","Sau"),
                                    new Answer("Senegal","Sen"),
                                    new Answer("Serbia and Montenegro","Ser"),
                                    new Answer("Seychelles","Sey"),
                                    new Answer("Sierra Leone","Sie"),
                                    new Answer("Singapore","Sin"),
                                    new Answer("Slovakia","Slo"),
                                    new Answer("Slovenia","Slov"),
                                    new Answer("Solomon Islands","Sol"),
                                    new Answer("Somalia","Som"),
                                    new Answer("South Africa","Sou"),
                                    new Answer("South Georgia and the South Sandwich Islands","SouG"),
                                    new Answer("Spain","Spa"),
                                    new Answer("Sri Lanka","Sri"),
                                    new Answer("Sudan","Sud"),
                                    new Answer("Suriname","Sur"),
                                    new Answer("Svalbard and Jan Mayen","Sva"),
                                    new Answer("Swaziland","Swa"),
                                    new Answer("Sweden","Swe"),
                                    new Answer("Switzerland","Swi"),
                                    new Answer("Syrian Arab Republic","Syr"),
                                    new Answer("Taiwan, Province of China","Tai"),
                                    new Answer("Tajikistan","Taj"),
                                    new Answer("Tanzania, United Republic of","Tan"),
                                    new Answer("Thailand","Tha"),
                                    new Answer("Timor-Leste","Tim"),
                                    new Answer("Togo","Tog"),
                                    new Answer("Tokelau","Tok"),
                                    new Answer("Tonga","Ton"),
                                    new Answer("Trinidad and Tobago","Tri"),
                                    new Answer("Tunisia","Tun"),
                                    new Answer("Turkey","Tur"),
                                    new Answer("Turkmenistan","Turk"),
                                    new Answer("Turks and Caicos Islands","TurC"),
                                    new Answer("Tuvalu","Tuv"),
                                    new Answer("Uganda","Uga"),
                                    new Answer("Ukraine","Ukr"),
                                    new Answer("United Arab Emirates","Uni"),
                                    new Answer("United Kingdom","Unit"),
                                    new Answer("United States","UniS"),
                                    new Answer("United States Minor Outlying Islands","UniSM"),
                                    new Answer("Uruguay","Uru"),
                                    new Answer("Uzbekistan","Uzb"),
                                    new Answer("Vanuatu","Van"),
                                    new Answer("Venezuela","Ven"),
                                    new Answer("Viet Nam","Vie"),
                                    new Answer("Virgin Islands, British","Vir"),
                                    new Answer("Virgin Islands, US","VirI"),
                                    new Answer("Wallis and Futuna","Wal"),
                                    new Answer("Western Sahara","Wes"),
                                    new Answer("Yemen","Yem"),
                                    new Answer("Zambia","Zam"),
                                    new Answer("Zimbabwe","Zim"),

            };
        }
    }
}
