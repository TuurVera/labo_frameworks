using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_deel2.Model
{
    public class NewsMessage
    {
        public int? id { get; set; }

        [Required]
        public string title { get; set; }


        [Required]
        public string message { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
