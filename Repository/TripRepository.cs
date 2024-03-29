using UbbRentalBike.Data;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Models;

namespace UbbRentalBike.Repository;

public class TripRepository : ITripRepository
{
        private readonly RentalContext _context;

        public TripRepository(RentalContext context)
        {
            _context = context;
        }

        public IEnumerable<Trip> GetAll()
        {
            return _context.Trips.ToList();
        }

        public Trip GetById(int id)
        {
            return _context.Trips.Find(id);
        }

        public void Insert(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip));
            }

            _context.Trips.Add(trip);
            Save();
        }

        public void Update(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip));
            }

            _context.Entry(trip).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            var trip = GetById(id);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }