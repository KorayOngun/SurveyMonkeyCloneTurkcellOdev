using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class AnswerRequest : IDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }

        public IList<MultiChoiceForAnswerRequest> MultiChoiceAnswer { get; set; } = new List<MultiChoiceForAnswerRequest>();
        public IList<SingleChoiceForAnswerRequest> SingleChoiceAnswer { get; set; } = new List<SingleChoiceForAnswerRequest>();
        public IList<LineResponseForAnswerRequest> lineAnswers { get; set; } = new List<LineResponseForAnswerRequest>();
    }
}
