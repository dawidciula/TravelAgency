using System;
using System.Collections.Generic;
using System.Linq;
using UbbRentalBike.Models;

namespace UbbRentalBike.Data
{
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
                    DateOfBirth = new DateTime(1998, 4, 22),
                    EmailAddress = "jankowalski@mail.com",
                    Reservation = new List<Reservation>()
                },
                new Participant
                {
                    Name = "Adam",
                    Surname = "Nowak",
                    DateOfBirth = new DateTime(2001, 8, 23),
                    EmailAddress = "adamnowak@email.com",
                    Reservation = new List<Reservation>()
                },
                new Participant
                {
                    Name = "Julia",
                    Surname = "Szyszka",
                    DateOfBirth = new DateTime(1999, 8, 12),
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
                    StartDate = new DateTime(2024, 7, 10),
                    EndData = new DateTime(2024, 7, 17),
                    PlaceOfDeparture = "Katowice",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Zwiedź Paryż!",
                    Destination = "Paryż",
                    StartDate = new DateTime(2024, 7, 3),
                    EndData = new DateTime(2024, 7, 13),
                    PlaceOfDeparture = "Gdańsk",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Odwiedź Teneryfe!",
                    Destination = "Teneryfa",
                    StartDate = new DateTime(2024, 7, 15),
                    EndData = new DateTime(2024, 7, 22),
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
                    ReservationDate = new DateTime(2024, 1, 22),
                    ParticipantId = participants[0].Id,
                    TripId = trips[2].Id
                },
                new Reservation
                {
                    ReservationDate = new DateTime(2023, 9, 25),
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
}
