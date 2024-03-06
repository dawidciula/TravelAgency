namespace UbbRentalBike.Models;

public class Reservations
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int TripId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int NumberOfPeople { get; set; }
    
    public Participants Particpant { get; set; }
    public Trips Trip { get; set; }
}