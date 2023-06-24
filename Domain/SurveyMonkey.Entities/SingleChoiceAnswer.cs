using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class SingleChoiceAnswer : IEntity
    {
        public int AnswerId { get; set; }
        public Answer? Answer { get; set; }  
        
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        public int ChoiceId { get; set; }
        public Choice? Choice { get; set; }  
    }
}
