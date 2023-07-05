using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class MultiChoiceForAnswerRequest : IVirtualDto
    {
        public int QuestionId { get; set; }
        public int ChoiceId { get; set; }
    }
}
