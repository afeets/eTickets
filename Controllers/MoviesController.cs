using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        // Inject
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
                
            return View(allMovies);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            // check if searchString matches
            if(!string.IsNullOrEmpty(searchString))
            {
                // Search filter, changeing search case to lowercase
                var filteredResult = allMovies.Where( n => n.Name.ToLower().Contains(searchString.ToLower()) || 
                    n.Description.ToLower().Contains (searchString.ToLower())).ToList();
                
                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        // GET: Movies/Details/[id]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieViewModel movie)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownValues();

                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        

         // GET: Movies/Edit[id]
        public async Task<IActionResult> Edit(int id)
        {
            // get movie details
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null) return View("NotFound");

            var response = new NewMovieViewModel()
            {
                Id              = movieDetails.Id,
                Name            = movieDetails.Name,
                Description     = movieDetails.Description,
                Price           = movieDetails.Price,
                StartDate       = movieDetails.StartDate,
                EndDate         = movieDetails.EndDate,
                ImageUrl        = movieDetails.ImageUrl,
                MovieCategory   = movieDetails.MovieCategory,
                CinemaId        = movieDetails.CinemaId,
                ProducerId      = movieDetails.ProducerId,
                ActorIds        = movieDetails.Actors_Movies.Select( n => n.ActorId).ToList()
            };
            
            var movieDropdownData = await _service.GetNewMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieViewModel movie)
        {
            // check if Id matches movie
            if(id != movie.Id) return View("NotFound");
            
            if(!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownValues();

                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}