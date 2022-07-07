using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task <Movie> GetMovieByIdAsync(int id);
        Task <NewMovieDropdownViewModel> GetNewMovieDropdownValues();

        Task AddNewMovieAsync(NewMovieViewModel data);
        Task UpdateMovieAsync(NewMovieViewModel data);
    }
}