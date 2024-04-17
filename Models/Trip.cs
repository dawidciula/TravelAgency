namespace UbbRentalBike.Models;
using FluentValidation;

public class Trip
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndData { get; set; }
    public string PlaceOfDeparture { get; set; }
    public string Destination { get; set; }
    
    public List<Reservation>? Reservation { get; set; }
}

public class TripValidation : AbstractValidator<Trip>
{
    public TripValidation()
    {
        RuleFor(trip => trip.TripName)
            .NotEmpty().WithMessage("Nazwa wycieczki jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa wycieczki nie może przekraczać 100 znaków.");

        RuleFor(trip => trip.StartDate)
            .NotEmpty().WithMessage("Data rozpoczęcia wycieczki jest wymagana.")
            .Must((trip, startDate) => startDate < trip.EndData).WithMessage("Data rozpoczęcia musi być wcześniejsza niż data zakończenia.");

        RuleFor(trip => trip.EndData)
            .NotEmpty().WithMessage("Data zakończenia wycieczki jest wymagana.")
            .Must((trip, endData) => endData > trip.StartDate).WithMessage("Data zakończenia musi być późniejsza niż data rozpoczęcia.");

        RuleFor(trip => trip.PlaceOfDeparture)
            .NotEmpty().WithMessage("Miejsce wyjazdu jest wymagane.")
            .MaximumLength(100).WithMessage("Miejsce wyjazdu nie może przekraczać 100 znaków.");

        RuleFor(trip => trip.Destination)
            .NotEmpty().WithMessage("Cel podróży jest wymagany.")
            .MaximumLength(100).WithMessage("Cel podróży nie może przekraczać 100 znaków.");
    }
}