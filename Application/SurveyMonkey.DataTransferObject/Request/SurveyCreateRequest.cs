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
        [Required]
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddDays(7);
        [Required]
        public IList<QuestionForSurveyCreate> Questions { get; set; } = new List<QuestionForSurveyCreate>();
    }
}
