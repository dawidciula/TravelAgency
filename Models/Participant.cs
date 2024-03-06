namespace UbbRentalBike.Models;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    
    public List<Reservation> Reservation { get; set; }
}