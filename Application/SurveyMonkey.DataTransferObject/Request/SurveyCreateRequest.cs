using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class SurveyCreateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<QuestionForSurveyCreateRequest> Questions { get; set; } = new List<QuestionForSurveyCreateRequest>();
    }
}
