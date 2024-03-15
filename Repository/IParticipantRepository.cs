using UbbRentalBike.Models;

namespace UbbRentalBike.Repository;

public interface IParticipantRepository
{
    IEnumerable<Participant> GetAll();
    Participant GetById(int Id);
    void Insert(Participant participant);
    void Update(Participant participant);
    void Delete(int Id);
    void Save();
}