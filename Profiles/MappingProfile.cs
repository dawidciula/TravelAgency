using AutoMapper;
using UbbRentalBike.Models;
using UbbRentalBike.ViewModels;

namespace UbbRentalBike.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Participant, ParticipantDto>();
        CreateMap<ParticipantDto, Participant>();
        
        CreateMap<Reservation, ReservationDto>();
        CreateMap<ReservationDto, Reservation>();

        CreateMap<Trip, TripDto>();
        CreateMap<TripDto, Trip>();
    }
}