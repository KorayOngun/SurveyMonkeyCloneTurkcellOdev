using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Request
{
    public class SurveyCreateRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public IList<QuestionForSurveyCreate> Questions { get; set; } = new List<QuestionForSurveyCreate>();
    }
}
