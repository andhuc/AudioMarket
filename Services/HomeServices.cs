using Microsoft.EntityFrameworkCore;
using PRN_Project.Models;

namespace PRN_Project.Services
{
    public class HomeServices
    {

        public Audio findMostLiked()
        {
            Favorite favorite = new AudioMarketContext().Favorites
                .FromSqlRaw("select audioId, COUNT(userId) as userId from Favorite group by audioId")
                .OrderByDescending(f => f.userId)
                .FirstOrDefault();

            return favorite != null ? favorite.Audio : null;
        }

    }
}
