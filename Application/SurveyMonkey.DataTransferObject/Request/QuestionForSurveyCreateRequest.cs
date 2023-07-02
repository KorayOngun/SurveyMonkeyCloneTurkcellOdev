using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class QuestionForSurveyCreateRequest :IVirtualDto
    {
        public string Text { get; set; }
        public int QuestionTypeId { get; set; }
        public IList<ChoiceForSurveyCreateRequest> Choices { get; set; } = new List<ChoiceForSurveyCreateRequest>();
    }
}
