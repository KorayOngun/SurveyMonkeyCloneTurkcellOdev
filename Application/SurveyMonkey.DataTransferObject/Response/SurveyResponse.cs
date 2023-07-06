using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyResponse : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IList<QuestionView> Questions { get; set; } = new List<QuestionView>();
    }
}
