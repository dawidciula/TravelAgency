using Microsoft.AspNetCore.Mvc;
using UbbRentalBike.Models;
using UbbRentalBike.Services;

namespace UbbRentalBike.Controllers
{
    public class TripServiceController : Controller
    {
        private readonly ITripService _tripService;
        private const int PageSize = 10;

        public TripServiceController(ITripService tripService)
        {
            _tripService = tripService;
        }

        public IActionResult Index(int page = 1, string nameFilter = null, int? tripId = null)
        {
            if (tripId.HasValue)
            {
                var trip = _tripService.GetTripById(tripId.Value);
                return View("SearchById", trip);
            }
            else if (!string.IsNullOrEmpty(nameFilter))
            {
                var trips = _tripService.GetTripByName(nameFilter);
                return View("SearchByName", trips);
            }
            else
            {
                var trips = _tripService.GetTripsPerPage(page, PageSize);
                return View(trips);
            }
        }
    }
}