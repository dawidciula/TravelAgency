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
                },
                new Trip
                {
                    TripName = "Odkryj Rzym!",
                    Destination = "Rzym",
                    StartDate = new DateTime(2024, 8, 5),
                    EndData = new DateTime(2024, 8, 12),
                    PlaceOfDeparture = "Kraków",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Zwiedzaj Londyn!",
                    Destination = "Londyn",
                    StartDate = new DateTime(2024, 8, 20),
                    EndData = new DateTime(2024, 8, 28),
                    PlaceOfDeparture = "Wrocław",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Podróżuj po Barcelonie!",
                    Destination = "Barcelona",
                    StartDate = new DateTime(2024, 9, 10),
                    EndData = new DateTime(2024, 9, 18),
                    PlaceOfDeparture = "Szczecin",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Wypoczywaj na Malcie!",
                    Destination = "Malta",
                    StartDate = new DateTime(2024, 9, 25),
                    EndData = new DateTime(2024, 10, 2),
                    PlaceOfDeparture = "Łódź",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Poznaj Amsterdam!",
                    Destination = "Amsterdam",
                    StartDate = new DateTime(2024, 10, 10),
                    EndData = new DateTime(2024, 10, 17),
                    PlaceOfDeparture = "Poznań",
                    Reservation = new List<Reservation>()
                },
                new Trip
                {
                    TripName = "Odkryj Pragę!",
                    Destination = "Praga",
                    StartDate = new DateTime(2024, 10, 25),
                    EndData = new DateTime(2024, 11, 1),
                    PlaceOfDeparture = "Katowice",
                    Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Eksploruj Budapeszt!",
        Destination = "Budapeszt",
        StartDate = new DateTime(2024, 11, 5),
        EndData = new DateTime(2024, 11, 12),
        PlaceOfDeparture = "Gdańsk",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Słoneczna wycieczka do Lizbony!",
        Destination = "Lizbona",
        StartDate = new DateTime(2024, 11, 20),
        EndData = new DateTime(2024, 11, 27),
        PlaceOfDeparture = "Warszawa",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Relaks na Wyspach Kanaryjskich!",
        Destination = "Fuerteventura",
        StartDate = new DateTime(2024, 12, 5),
        EndData = new DateTime(2024, 12, 12),
        PlaceOfDeparture = "Kraków",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Odwiedź Kopenhagę!",
        Destination = "Kopenhaga",
        StartDate = new DateTime(2024, 12, 20),
        EndData = new DateTime(2024, 12, 27),
        PlaceOfDeparture = "Wrocław",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Zimowy wypoczynek w Innsbrucku!",
        Destination = "Innsbruck",
        StartDate = new DateTime(2025, 1, 5),
        EndData = new DateTime(2025, 1, 12),
        PlaceOfDeparture = "Poznań",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Wycieczka do Madrytu!",
        Destination = "Madryt",
        StartDate = new DateTime(2025, 1, 20),
        EndData = new DateTime(2025, 1, 27),
        PlaceOfDeparture = "Szczecin",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Zwiedzaj Florencję!",
        Destination = "Florencja",
        StartDate = new DateTime(2025, 2, 5),
        EndData = new DateTime(2025, 2, 12),
        PlaceOfDeparture = "Łódź",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Odkryj Edynburg!",
        Destination = "Edynburg",
        StartDate = new DateTime(2025, 2, 20),
        EndData = new DateTime(2025, 2, 27),
        PlaceOfDeparture = "Katowice",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Szlakiem Krakowa!",
        Destination = "Kraków",
        StartDate = new DateTime(2025, 3, 5),
        EndData = new DateTime(2025, 3, 12),
        PlaceOfDeparture = "Gdańsk",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Odwiedź Oslo!",
        Destination = "Oslo",
        StartDate = new DateTime(2025, 3, 20),
        EndData = new DateTime(2025, 3, 27),
        PlaceOfDeparture = "Warszawa",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Wypoczywaj na Malcie!",
        Destination = "Malta",
        StartDate = new DateTime(2025, 4, 5),
        EndData = new DateTime(2025, 4, 12),
        PlaceOfDeparture = "Kraków",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Zwiedzaj Ateny!",
        Destination = "Ateny",
        StartDate = new DateTime(2025, 4, 20),
        EndData = new DateTime(2025, 4, 27),
        PlaceOfDeparture = "Wrocław",
        Reservation = new List<Reservation>()
    },
    new Trip
    {
        TripName = "Odkrywaj Berlin!",
        Destination = "Berlin",
        StartDate = new DateTime(2025, 5, 5),
        EndData = new DateTime(2025, 5, 12),
        PlaceOfDeparture = "Poznań",
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
