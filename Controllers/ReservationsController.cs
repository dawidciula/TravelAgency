using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UbbRentalBike.Data;
using UbbRentalBike.Models;
using UbbRentalBike.ViewModels;

namespace UbbRentalBike.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly RentalContext _context;
        private readonly IValidator<Reservation> _reservationValidator;
        private readonly IMapper _mapper;

        public ReservationsController(RentalContext context, IValidator<Reservation> reservationValidator, IMapper mapper)
        {
            _context = context;
            _reservationValidator = reservationValidator;
            _mapper = mapper;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Particpant)
                .Include(r => r.Trip)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return View(reservationDtos);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Particpant)
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return View(reservationDto);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id");
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParticipantId,TripId,ReservationDate")] ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            var validationResult = _reservationValidator.Validate(reservation);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(reservationDto);
            }

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", reservation.ParticipantId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", reservation.TripId);
            return View(reservationDto);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservation);

            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", reservation.ParticipantId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", reservation.TripId);

            return View(reservationDto);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParticipantId,TripId,ReservationDate")] ReservationDto reservationDto)
        {
            if (id != reservationDto.Id)
            {
                return NotFound();
            }

            var reservation = _mapper.Map<Reservation>(reservationDto);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", reservation.ParticipantId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", reservation.TripId);
            return View(reservationDto);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Particpant)
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservation);

            return View(reservationDto);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
