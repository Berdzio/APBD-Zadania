using cwiczenia8.Entites;

namespace cwiczenia8.Services
{
    public interface ITripDbService
    {
        Task<List<Trip>> GetTrip(CancellationToken cancellationToken = default);
    }
}
