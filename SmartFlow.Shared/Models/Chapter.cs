﻿using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Models.Ede.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public string ChapterClassId { get; set; }
        public DeclarationType ChapterDeclarationType { get; set; }
        public ChapterIdentifiers ChapterIdentifier { get; set; }
        public List<Question> Questions { get; set; }
    }
}