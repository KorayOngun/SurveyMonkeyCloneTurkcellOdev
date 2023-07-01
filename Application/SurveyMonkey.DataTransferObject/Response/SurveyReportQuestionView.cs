using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyReportQuestionView : IVirtualDto 
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public IList<SurveyReportChoicesView> Choices { get; set; } = new List<SurveyReportChoicesView>();
    }
}
