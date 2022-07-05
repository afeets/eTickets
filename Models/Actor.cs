using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Profile Picture is required")]
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }
        
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be greater than 3 Characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "Biography is required")]
        [Display(Name = "Biography")]
        public string Bio { get; set; }


        // Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
