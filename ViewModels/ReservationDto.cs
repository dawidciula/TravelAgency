using UbbRentalBike.Models;
namespace UbbRentalBike.ViewModels;

public class ReservationDto
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int TripId { get; set; }
    public DateTime ReservationDate { get; set; }
}