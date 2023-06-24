﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}
        public ICollection<Survey> Survey { get; set;} = new List<Survey>();    
    }
}
