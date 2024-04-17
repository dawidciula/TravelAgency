namespace UbbRentalBike.Models;
using FluentValidation;

public class Reservation
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int TripId { get; set; }
    public DateTime ReservationDate { get; set; }
    
    public Participant? Particpant { get; set; }
    public Trip? Trip { get; set; }
}

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(reservation => reservation.ParticipantId)
            .NotEmpty().WithMessage("Id uczestnika jest wymagane.");

        RuleFor(reservation => reservation.TripId)
            .NotEmpty().WithMessage("Id wycieczki jest wymagane.");
    }
}