using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
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
            var allActors = await _service.GetAllAsync();
            return View(allActors);
        }


        // Get: Request Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Details/[id]
        public async Task<IActionResult> Details(int id)
        {
            // Check if Actor exists
            var actorDetails = await _service.GetByIdAsync(id);

            if(actorDetails == null) return View("NotFound");
            return View(actorDetails);

        }


        // Get: Request Actors/Edit/[id]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if(actorDetails == null) return View("NotFound");
            
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }


        // Get: Request Actors/Delete/[id]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if(actorDetails == null) return View("NotFound");
            
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // check actor exists
            var actorDetails = await _service.GetByIdAsync(id);
            if(actorDetails == null) return View("NotFound");
            
            await _service.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
               

    }
}