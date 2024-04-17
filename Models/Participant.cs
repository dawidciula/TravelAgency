using FluentValidation;
namespace UbbRentalBike.Models;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    
    public List<Reservation>? Reservation { get; set; }
}

public class ParticipantValidator : AbstractValidator<Participant>
{
    public ParticipantValidator()
    {
        RuleFor(participant => participant.Name)
            .NotEmpty().WithMessage("Pole Imię jest wymagane.");

        RuleFor(participant => participant.Surname)
            .NotEmpty().WithMessage("Pole Nazwisko jest wymagane.");

        RuleFor(participant => participant.DateOfBirth)
            .NotEmpty().WithMessage("Pole Data urodzenia jest wymagane.");

        RuleFor(participant => participant.EmailAddress)
            .NotEmpty().WithMessage("Pole Adres email jest wymagane.")
            .EmailAddress().WithMessage("Nieprawidłowy format adresu email.");
    }
}