using Microsoft.EntityFrameworkCore;
using VideoGameCatalogueApi.Data;
using VideoGameCatalogueApi.Repositories.Interfaces;
using VideoGameCatalogueApi.Models;

namespace VideoGameCatalogueApi.Repositories
{
    /// <summary>
    /// Implementation of the IVideoGameRepository interface for interacting with the database
    /// </summary>
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly VideoGameContext _context;

        public VideoGameRepository(VideoGameContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all video games from the database
        /// </summary>
        /// <returns>Returns a collection of VideoGame objects</returns>
        public async Task<IEnumerable<VideoGame>> GetAllAsync()
        {
            return await _context.VideoGames.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single video game by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the VideoGame object if found, or null if not found</returns>
        public async Task<VideoGame> GetByIdAsync(int id)
        {
            return await _context.VideoGames.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously adds a new video game to the database
        /// </summary>
        /// <param name="videoGame"></param>
        public async Task AddAsync(VideoGame videoGame)
        {
            await _context.VideoGames.AddAsync(videoGame);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing video game in the database
        /// </summary>
        /// <param name="videoGame"></param>
        public async Task UpdateAsync(VideoGame videoGame)
        {
            _context.VideoGames.Update(videoGame);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes a video game from the database by its ID
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteAsync(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame != null)
            {
                _context.VideoGames.Remove(videoGame);
                await _context.SaveChangesAsync();
            }
        }
    }
}
