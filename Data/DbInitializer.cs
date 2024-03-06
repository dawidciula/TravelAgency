using UbbRentalBike.Models;

namespace UbbRentalBike.Data;

public class DbInitializer
{
    public static void Initialize(RentalContext context)
    {
        context.Database.EnsureCreated();

        if (context.Participants.Any())
        {
            return;
        }

        var participants = new Participant[]
        {
            new Participant
            {
                Name = "Jan",
                Surname = "Kowalski",
                DateOfBirth = DateTime.Parse("1998-22-04"),
                EmailAddress = "jankowalski@mail.com",
                Reservation = new List<Reservation>()
            },
            new Participant()
            {
                Name = "Adam",
                Surname = "Nowak",
                DateOfBirth = DateTime.Parse("28-03-2001"),
                EmailAddress = "adamnowak@email.com",
                Reservation = new List<Reservation>()
            },
            new Participant()
            {
                Name = "Julia",
                Surname = "Szyszka",
                DateOfBirth = DateTime.Parse("1999-08-12"),
                EmailAddress = "juliaszyszka@email.com",
                Reservation = new List<Reservation>()
            }
        };
        foreach (Participant p in participants)
        {
            context.Participants.Add(p);
        }

        context.SaveChanges();

        var trips = new Trip[]
        {
            new Trip
            {
                TripName = "Zobacz Mediolan!",
                Destination = "Mediolan",
                StartDate = DateTime.Parse("2024-07-10"),
                EndData = DateTime.Parse("2024-07-17"),
                PlaceOfDeparture = "Katowice",
                Reservation = new List<Reservation>()
            },
            new Trip
            {
                TripName = "Zwiedź Paryż!",
                Destination = "Paryż",
                StartDate = DateTime.Parse("2024-07-03"),
                EndData = DateTime.Parse("2024-07-13"),
                PlaceOfDeparture = "Gdańsk",
                Reservation = new List<Reservation>()
            },
            new Trip
            {
                TripName = "Odwiedź Teneryfe!",
                Destination = "Teneryfa",
                StartDate = DateTime.Parse("2024-07-15"),
                EndData = DateTime.Parse("2024-07-22"),
                PlaceOfDeparture = "Warszawa",
                Reservation = new List<Reservation>()
            }
        };
        foreach (Trip t in trips)
        {
            context.Trips.Add(t);
        }

        context.SaveChanges();

        var reservations = new Reservation[]
        {
            new Reservation
            {
                ReservationDate = DateTime.Parse("22-01-2024"),
                ParticipantId = participants[0].Id,
                TripId = trips[2].Id
            },
            new Reservation
            {
                ReservationDate = DateTime.Parse("2023-09-25"),
                ParticipantId = participants[1].Id,
                TripId = trips[0].Id
            }
        };
        foreach (Reservation r in reservations)
        {
            context.Reservations.Add(r);
        }

        context.SaveChanges();
    }
}