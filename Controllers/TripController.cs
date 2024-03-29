using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
using UbbRentalBike.Repository;

namespace UbbRentalBike.Controllers
{
    public class TripController : Controller
    {
       private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public IActionResult Index()
        {
            var trips = _tripRepository.GetAll();
            return View(trips);
        }

        public IActionResult Details(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TripName,StartDate,EndDate,PlaceOfDeparture,Destination")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _tripRepository.Insert(trip);
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        public IActionResult Edit(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TripName,StartDate,EndDate,PlaceOfDeparture,Destination")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tripRepository.Update(trip);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        public IActionResult Delete(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _tripRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _tripRepository.GetById(id) != null;
        }
    }
}
