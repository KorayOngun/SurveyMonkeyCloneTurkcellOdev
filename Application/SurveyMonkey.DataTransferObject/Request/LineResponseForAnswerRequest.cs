﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class LineResponseForAnswerRequest
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
