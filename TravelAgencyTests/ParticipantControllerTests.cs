using Moq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UbbRentalBike.Controllers;
using UbbRentalBike.Models;
using UbbRentalBike.Repository;
using UbbRentalBike.ViewModels;

public class ParticipantControllerTests
{
    private readonly Mock<IParticipantRepository> _participantRepositoryMock;
    private readonly Mock<IValidator<Participant>> _participantValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ParticipantController _controller;

    public ParticipantControllerTests()
    {
        _participantRepositoryMock = new Mock<IParticipantRepository>();
        _participantValidatorMock = new Mock<IValidator<Participant>>();
        _mapperMock = new Mock<IMapper>();
        _controller = new ParticipantController(
            _participantRepositoryMock.Object, 
            _participantValidatorMock.Object, 
            _mapperMock.Object);
    }

    [Fact]
    public void Index_ReturnsViewResult_WithListOfParticipants()
    {
        var participants = new List<Participant> { new Participant { Id = 1, Name = "John", Surname = "Doe" } };
        var participantDtos = new List<ParticipantDto> { new ParticipantDto { Id = 1, Name = "John", Surname = "Doe" } };
        _participantRepositoryMock.Setup(repo => repo.GetAll()).Returns(participants);
        _mapperMock.Setup(m => m.Map<List<ParticipantDto>>(participants)).Returns(participantDtos);
        
        var result = _controller.Index();
        
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<ParticipantDto>>(viewResult.Model);
        Assert.Single(model);
        Assert.Equal("John", model.First().Name);
    }

    [Fact]
    public void Details_ExistingId_ReturnsViewResult_WithParticipantDto()
    {
        var participant = new Participant { Id = 1, Name = "John", Surname = "Doe" };
        var participantDto = new ParticipantDto { Id = 1, Name = "John", Surname = "Doe" };
        _participantRepositoryMock.Setup(repo => repo.GetById(1)).Returns(participant);
        _mapperMock.Setup(m => m.Map<ParticipantDto>(participant)).Returns(participantDto);
        
        var result = _controller.Details(1);
        
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ParticipantDto>(viewResult.Model);
        Assert.Equal("John", model.Name);
    }

    [Fact]
    public void Details_NonExistingId_ReturnsNotFoundResult()
    {
        _participantRepositoryMock.Setup(repo => repo.GetById(1)).Returns((Participant)null);
        
        var result = _controller.Details(1);
        
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreatePost_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var participantDto = new ParticipantDto { Name = "John", Surname = "Doe" };
        var participant = new Participant { Name = "John", Surname = "Doe" };
        _mapperMock.Setup(m => m.Map<Participant>(participantDto)).Returns(participant);
        _participantValidatorMock.Setup(v => v.Validate(participant)).Returns(new ValidationResult());
        
        var result = _controller.Create(participantDto);
        
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(ParticipantController.Index), redirectToActionResult.ActionName);
        _participantRepositoryMock.Verify(repo => repo.Insert(participant), Times.Once);
    }

    [Fact]
    public void CreatePost_InvalidModel_ReturnsViewResult_WithModelErrors()
    {
        var participantDto = new ParticipantDto { Name = "John", Surname = "Doe" };
        var participant = new Participant { Name = "John", Surname = "Doe" };
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });
        _mapperMock.Setup(m => m.Map<Participant>(participantDto)).Returns(participant);
        _participantValidatorMock.Setup(v => v.Validate(participant)).Returns(validationResult);
        
        var result = _controller.Create(participantDto);
        
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(participantDto, viewResult.Model);
        Assert.True(_controller.ModelState.ContainsKey("Name"));
        Assert.Equal("Name is required", _controller.ModelState["Name"].Errors.First().ErrorMessage);
        _participantRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Participant>()), Times.Never);
    }
}
