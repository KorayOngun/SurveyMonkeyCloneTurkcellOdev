using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
       
        public IList<MultiChoiceAnswer> MultiChoiceAnswer { get; set; } = new List<MultiChoiceAnswer>();
        public IList<SingleChoiceAnswer> SingleChoiceAnswer { get; set; } = new List<SingleChoiceAnswer>();
        public IList<LineAnswer> lineAnswers { get; set; } = new List<LineAnswer>();
    }
}
