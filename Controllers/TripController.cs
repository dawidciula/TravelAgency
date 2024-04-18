using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UbbRentalBike.Models;
using UbbRentalBike.Repository;
using UbbRentalBike.ViewModels;

namespace UbbRentalBike.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository _tripRepository;
        private readonly IValidator<Trip> _tripValidator;
        private readonly IMapper _mapper;

        public TripController(ITripRepository tripRepository, IValidator<Trip> tripValidator, IMapper mapper)
        {
            _tripRepository = tripRepository;
            _tripValidator = tripValidator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var trips = _tripRepository.GetAll();
            var tripDtos = _mapper.Map<IEnumerable<TripDto>>(trips);
            return View(tripDtos);
        }

        public IActionResult Details(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }

            var tripDto = _mapper.Map<TripDto>(trip);
            return View(tripDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TripName,StartDate,EndDate,PlaceOfDeparture,Destination")] TripDto tripDto)
        {
            var trip = _mapper.Map<Trip>(tripDto);

            var validationResult = _tripValidator.Validate(trip);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(tripDto);
            }

            _tripRepository.Insert(trip);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }

            var tripDto = _mapper.Map<TripDto>(trip);
            return View(tripDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TripName,StartDate,EndDate,PlaceOfDeparture,Destination")] TripDto tripDto)
        {
            if (id != tripDto.Id)
            {
                return NotFound();
            }

            var trip = _mapper.Map<Trip>(tripDto);

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
            return View(tripDto);
        }

        public IActionResult Delete(int id)
        {
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }

            var tripDto = _mapper.Map<TripDto>(trip);
            return View(tripDto);
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
