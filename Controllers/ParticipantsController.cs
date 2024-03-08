using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Data;
using UbbRentalBike.Models;

namespace UbbRentalBike.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly RentalContext _context;
        

        public ParticipantsController(RentalContext context)
        {
            _context = context;
        }



        // GET: Participants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participants.ToListAsync());
        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        // GET: Participants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,DateOfBirth,EmailAddress")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // GET: Participants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,DateOfBirth,EmailAddress")] Participant participant)
        {
            if (id != participant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantExists(participant.Id))
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
            return View(participant);
        }

        // GET: Participants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.Id == id);
        }
    }
}
