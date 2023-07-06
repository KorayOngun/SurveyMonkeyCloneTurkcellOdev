using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Entities
{
    public class Survey : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; }
        public  int UserId { get; set; }
        public User? User { get; set; }
        

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public IList<Question> Questions { get; set; } = new List<Question>();
    }
}
