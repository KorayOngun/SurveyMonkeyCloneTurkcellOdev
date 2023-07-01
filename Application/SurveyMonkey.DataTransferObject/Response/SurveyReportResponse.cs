using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyReportResponse
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public IList<SurveyReportQuestionView> Questions { get; set; }
    }
}
