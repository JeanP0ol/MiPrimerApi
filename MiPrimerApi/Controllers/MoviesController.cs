using MiPrimerApi.DAL.Models.Dtos;
using Api.W.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.W.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMoviesAsync()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieAsync(id);
                return Ok(movie);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _movieService.CreateMovieAsync(dto);

            // Validación para evitar error si el ID no se genera correctamente
            if (created == null || created.Id == 0)
                return Ok(created);

            return CreatedAtAction(nameof(GetMovieAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MovieDto>> UpdateMovieAsync([FromBody] MovieCreateUpdateDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _movieService.UpdateMovieAsync(dto, id);
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                var deleted = await _movieService.DeleteMovieAsync(id);
                return Ok(deleted);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}

