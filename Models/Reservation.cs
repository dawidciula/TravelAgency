namespace UbbRentalBike.Models;

public class Reservation
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int TripId { get; set; }
    public DateTime ReservationDate { get; set; }
    
    public Participant Particpant { get; set; }
    public Trip Trip { get; set; }
}