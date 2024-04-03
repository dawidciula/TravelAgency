using UbbRentalBike.Models;

namespace UbbRentalBike.Repository;

public interface ITripRepository
{
    IEnumerable<Trip> GetAll();
    Trip GetById(int Id);
    void Insert(Trip trip);
    void Update(Trip trip);
    void Delete(int Id);
    void Save();
}