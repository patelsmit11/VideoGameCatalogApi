using Microsoft.EntityFrameworkCore;
using VideoGameCatalogueApi.Models;

namespace VideoGameCatalogueApi.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options)
        : base(options) { }

        public DbSet<VideoGame> VideoGames { get; set; }
    }
}
