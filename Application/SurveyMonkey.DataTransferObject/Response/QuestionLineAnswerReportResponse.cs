using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class QuestionLineAnswerReportResponse : IDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<LineAnswerView> lineAnswers { get; set; }
    }
}
