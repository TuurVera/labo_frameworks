using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labo5_ASP_MVC.Models
{
    public class NewsMessage
    {
        public int? Id { get; set; }

        [Display(Name = "Titel")]
        [Required(ErrorMessage = "Titel vereist.")]
        public string Title { get; set; }

        [Display(Name = "Bericht")]
        [Required(ErrorMessage = "Bericht vereist.")]
        public string Message { get; set; }
		
        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum vereist.")]
		public DateTime Date { get; set; }

    }
}
