﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class ChoiceForSurveyCreate : IVirtualDto
    {
        public string Text { get; set; }
    }
}
