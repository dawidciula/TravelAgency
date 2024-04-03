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
            var trips = _tripRepository.GetAll(); // Pobierz wszystkie wycieczki
            int skip = (page - 1) * tripsPerPage; // Oblicz liczbę wycieczek do pominięcia

            // Zastosuj filtr, jeśli został podany
            if (!string.IsNullOrEmpty(filter))
            {
                trips = trips.Where(t => t.TripName.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            // Pobierz wycieczki na aktualnej stronie
            var currentPageTrips = trips.Skip(skip).Take(tripsPerPage);

            return currentPageTrips;
        }

        public Trip GetTripByName(string name)
        {
            var trips = _tripRepository.GetAll(); // Pobierz wszystkie wycieczki

            // Przeszukaj kolekcję wycieczek w poszukiwaniu wycieczki o podanej nazwie
            foreach (var trip in trips)
            {
                if (trip.TripName == name)
                {
                    return trip; // Zwróć wycieczkę, jeśli jej nazwa pasuje
                }
            }

            return null; // Jeśli nie znaleziono wycieczki o podanej nazwie, zwróć null lub rzuć wyjątek, w zależności od Twoich wymagań
        }

        
    }
}