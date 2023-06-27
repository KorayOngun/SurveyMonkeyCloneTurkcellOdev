using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class MultiChoiceAnswer : IEntity
    {
        public int AnswerId { get; set; }
   
        public int QuestionId { get; set; }

        public int ChoiceId { get; set; }
        
    }
}
