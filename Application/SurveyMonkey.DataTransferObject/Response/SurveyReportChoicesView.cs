using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyReportChoicesView : IVirtualDto
    {
        public int ChoiceId { get; set; }   
        public string Text { get; set; }
        public int Count { get; set; } = 0;
    }
}
