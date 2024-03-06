namespace UbbRentalBike.Models;

public class Participants
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime EmailAddress { get; set; }
    
    public List<Reservations> Reservation { get; set; }
}