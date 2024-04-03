using UbbRentalBike.Models;

namespace UbbRentalBike.Services;

public interface ITripService
{
    Trip GetTripById(int id);
    IEnumerable<Trip> GetTripsPerPage(int page, int tripsPerPage, string filter = "");
    Trip GetTripByName(string name);
}