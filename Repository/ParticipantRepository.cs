using UbbRentalBike.Data;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;

namespace UbbRentalBike.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly RentalContext _context;

        public ParticipantRepository(RentalContext context)
        {
            _context = context;
        }

        public IEnumerable<Participant> GetAll()
        {
            return _context.Participants.ToList();
        }

        public Participant GetById(int id)
        {
            return _context.Participants.Find(id);
        }

        public void Insert(Participant participant)
        {
            if (participant == null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            _context.Participants.Add(participant);
            Save();
        }

        public void Update(Participant participant)
        {
            if (participant == null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            _context.Entry(participant).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            var participant = GetById(id);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}