using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyReportResponse : IDto
    {
        public int SurveyId { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Participant { get; set; } 
        public string SurveyName { get; set; }
        public IList<SurveyReportQuestionView> Questions { get; set; }
    }
}
