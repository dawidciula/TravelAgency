using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
using UbbRentalBike.Repository;

namespace UbbRentalBike.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantController(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public IActionResult Index()
        {
            var participants = _participantRepository.GetAll();
            return View(participants);
        }

        public IActionResult Details(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Surname,DateOfBirth,EmailAddress")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                _participantRepository.Insert(participant);
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        public IActionResult Edit(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,DateOfBirth,EmailAddress")] Participant participant)
        {
            if (id != participant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _participantRepository.Update(participant);
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

        public IActionResult Delete(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _participantRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantExists(int id)
        {
            return _participantRepository.GetById(id) != null;
        }
    }
}
