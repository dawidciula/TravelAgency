using UbbRentalBike.Models;
using UbbRentalBike.Repository;

namespace UbbRentalBike.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public Trip GetTripById(int id)
        {
            return _tripRepository.GetById(id);
        }

        public IEnumerable<Trip> GetTripsPerPage(int page, int tripsPerPage, string filter = "")
        {
            var trips = _tripRepository.GetAll();
            
            int skip = (page - 1) * tripsPerPage; 
            
            if (!string.IsNullOrEmpty(filter))
            {
                trips = trips.Where(t => t.TripName.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }
            
            var currentPageTrips = trips.Skip(skip).Take(tripsPerPage);

            return currentPageTrips;
        }

        public Trip GetTripByName(string name)
        {
            var trips = _tripRepository.GetAll();
            
            foreach (var trip in trips)
            {
                if (trip.TripName == name)
                {
                    return trip;
                }
            }

            return null;
        }

        
    }
}