using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataTransferObject.Response
{
    public class SurveyListResponse : IDto
    {
        public int Id { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Name { get; set; }
    }
}
