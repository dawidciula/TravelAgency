using UbbRentalBike.Models;
namespace UbbRentalBike.ViewModels;


public class ParticipantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
}