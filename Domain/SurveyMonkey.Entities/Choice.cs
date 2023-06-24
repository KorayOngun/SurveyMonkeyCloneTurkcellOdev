﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class Choice : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }    
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

    }
}
