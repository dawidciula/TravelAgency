using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UbbRentalBike.Models;

public class Trips
{
    public int Id { get; set; }
    public string TripName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndData { get; set; }
    public string PlaceOfDeparture { get; set; }
    public string Destination { get; set; }
    
    public List<Reservations> Reservation { get; set; }
}