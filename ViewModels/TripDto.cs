using UbbRentalBike.Models;
namespace UbbRentalBike.ViewModels;

public class TripDto
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndData { get; set; }
    public string PlaceOfDeparture { get; set; }
    public string Destination { get; set; }
    
}