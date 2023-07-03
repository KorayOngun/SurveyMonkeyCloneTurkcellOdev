using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class QuestionView : IVirtualDto
    {
        public int? Id { get; set; }

        public string? Text { get; set; }

        public int? SurveyId { get; set; }

        public int? QuestionTypeId { get; set; }
        public string? QuestionTypeName { get; set; }
        public IList<Choice> Choices { get; set; } = new List<Choice>();
    }
}
