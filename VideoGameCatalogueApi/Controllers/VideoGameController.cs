using Microsoft.AspNetCore.Mvc;
using VideoGameCatalogueApi.Repositories.Interfaces;
using VideoGameCatalogueApi.Models;

namespace VideoGameCatalogueApi.Controllers
{
    /// <summary>
    /// Defining the route for this controller, allowing API calls for /api/videogame
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameRepository _repository;

        public VideoGameController(IVideoGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET api/videogame - Fetches all video games from the repository
        /// </summary>
        /// <returns>
        /// Returns the games as a successful response (200 OK) and 
        /// if an error occurs, return a 500 Internal Server Error with the exception details
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGame>>> GetVideoGames()
        {
            try
            {
                var games = await _repository.GetAllAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching video games.", Details = ex.Message });
            }
        }

        /// <summary>
        /// GET api/videogame/{id} - Fetches a single video game by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Return a 404 Not Found response if the game is not found
        /// Return the game with a 200 OK status
        /// Return a 500 Internal Server Error if something goes wrong
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGame(int id)
        {
            try
            {
                var game = await _repository.GetByIdAsync(id);
                if (game == null)
                {
                    return NotFound(new { Message = $"Video game with ID {id} not found." });
                }
                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching the video game.", Details = ex.Message });
            }
        }

        /// <summary>
        /// POST api/videogame - Adds a new video game to the repository
        /// </summary>
        /// <param name="videoGame"></param>
        /// <returns>
        /// Return 400 Bad Request with validation errors
        /// Return a 201 Created response indicating successful creation
        /// Return a 500 Internal Server Error if an error occurs
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> PostVideoGame(VideoGame videoGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _repository.AddAsync(videoGame);
                return CreatedAtAction(nameof(GetVideoGame), new { id = videoGame.Id }, videoGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the video game.", Details = ex.Message });
            }
        }

        /// <summary>
        /// PUT api/videogame/{id} - Updates an existing video game by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="videoGame"></param>
        /// <returns>
        /// Return a 204 No Content status indicating the update was successful
        /// Return a 500 Internal Server Error if an error occurs
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoGame(int id, VideoGame videoGame)
        {
            try
            {
                if (id != videoGame.Id)
                {
                    return BadRequest(new { Message = "Video game ID in the URL does not match the ID in the body." });
                }

                await _repository.UpdateAsync(videoGame);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the video game.", Details = ex.Message });
            }
        }

        /// <summary>
        /// DELETE api/videogame/{id} - Deletes a video game by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Return a 204 No Content status indicating successful deletion
        /// Return a 500 Internal Server Error if an error occurs
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the video game.", Details = ex.Message });
            }
        }
    }
}
