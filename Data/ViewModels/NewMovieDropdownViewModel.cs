using System.Collections.Generic;
using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownViewModel
    {
        public NewMovieDropdownViewModel()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; } 
    }
}