using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class NewMovieViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Must enter movie name")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Must enter movie description")]
        [Display(Name = "Movie description")]
        public string Description { get; set; }
        
        
        [Required(ErrorMessage = "Must enter price of movie")]
        [Display(Name = "Price in $")]
        public double Price { get; set; }
         
        [Required(ErrorMessage = "Movie Poster URL is required")]
        [Display(Name = "Movie Poster URL")]
        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Select a Category")]
        public MovieCategory MovieCategory { get; set; }


        // Relationships

        
        [Required(ErrorMessage = "Must add movie actor(s)")]
        [Display(Name = "Select actor(s)")]
        public List<int> ActorIds { get; set; }
        
        [Required(ErrorMessage = "Must select cinema")]
        [Display(Name = "Select a cinema")]
        public int CinemaId { get; set; }
        
        [Required(ErrorMessage = "Must add a Producer")]
        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }

    }
}
