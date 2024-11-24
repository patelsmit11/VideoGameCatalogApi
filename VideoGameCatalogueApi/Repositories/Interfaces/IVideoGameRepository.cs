using VideoGameCatalogueApi.Models;

namespace VideoGameCatalogueApi.Repositories.Interfaces
{
    /// <summary>
    /// Interface defining the contract for a repository that handles operations on VideoGame entities
    /// </summary>
    public interface IVideoGameRepository
    {
        // Asynchronously retrieves all video games from the data source
        Task<IEnumerable<VideoGame>> GetAllAsync();

        // Asynchronously retrieves a single video game by its ID
        Task<VideoGame> GetByIdAsync(int id);

        // Asynchronously adds a new video game to the data source
        Task AddAsync(VideoGame videoGame);

        // Asynchronously updates an existing video game in the data source
        Task UpdateAsync(VideoGame videoGame);

        // Asynchronously deletes a video game by its ID from the data source
        Task DeleteAsync(int id);
    }
}
