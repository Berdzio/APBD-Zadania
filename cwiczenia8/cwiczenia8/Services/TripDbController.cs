using cwiczenia8.Entites;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia8.Services
{
    public class TripDbService : ITripDbService
    {
        private readonly ApbdContext _apbdContext;


        public TripDbService(ApbdContext context)
        {
            _apbdContext = context;
        }
        public async Task<List<Trip>> GetTrip(CancellationToken cancellationToken = default)
        {
            var st = await _apbdContext.Trips.OrderByDescending(x => x.DateFrom).ToListAsync();
            return st;
        }
    }
}
