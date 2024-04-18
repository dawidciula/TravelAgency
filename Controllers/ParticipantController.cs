using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;
using UbbRentalBike.Repository;
using UbbRentalBike.ViewModels;

namespace UbbRentalBike.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IValidator<Participant> _participantValidator;
        private readonly IMapper _mapper;

        public ParticipantController(IParticipantRepository participantRepository, IValidator<Participant> participantValidator, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _participantValidator = participantValidator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var participants = _participantRepository.GetAll();
            var participantDtos = _mapper.Map<List<ParticipantDto>>(participants);
            return View(participantDtos);
        }

        public IActionResult Details(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }

            var participantDto = _mapper.Map<ParticipantDto>(participant);
            return View(participantDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Surname,DateOfBirth,EmailAddress")] ParticipantDto participantDto)
        {
            var participant = _mapper.Map<Participant>(participantDto);

            var validationResult = _participantValidator.Validate(participant);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(participantDto);
            }

            _participantRepository.Insert(participant);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }
            var participantDto = _mapper.Map<ParticipantDto>(participant);
            return View(participantDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,DateOfBirth,EmailAddress")] ParticipantDto participantDto)
        {
            if (id != participantDto.Id)
            {
                return NotFound();
            }

            var participant = _mapper.Map<Participant>(participantDto);

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
            return View(participantDto);
        }

        public IActionResult Delete(int id)
        {
            var participant = _participantRepository.GetById(id);
            if (participant == null)
            {
                return NotFound();
            }

            var participantDto = _mapper.Map<ParticipantDto>(participant);
            return View(participantDto);
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
