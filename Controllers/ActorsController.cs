using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        // private readonly AppDbContext _context;
        private readonly IActorsService _service;
        
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var allActors = await _service.GetAll();
            return View(allActors);
        }
    }
}