using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Models
{
    //by default is internal, and cannot be used outside of the project, so must change to public since we do want to use outside of the project in the web project
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Display(Name="Type Of Food")]
        public CuisineType Cuisine { get; set; }
    }
}
