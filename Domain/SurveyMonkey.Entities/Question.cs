using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
           
        public string Text { get; set; }

        public int SurveyId { get; set; }
        

        public int QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
        public IList<Choice>? Choices { get; set; }
    }
}
