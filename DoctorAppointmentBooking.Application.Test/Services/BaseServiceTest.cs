using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Application.Services;

public class BaseServiceTest
{
    private readonly Mock<IBaseRepository<TestEntity>> _baseRepositoryMock;
    private readonly BaseService<TestEntity> _baseService;

    public BaseServiceTest()
    {
        _baseRepositoryMock = new Mock<IBaseRepository<TestEntity>>();
        _baseService = new BaseService<TestEntity>(_baseRepositoryMock.Object);
    }

    [Fact]
    public void Add_ValidEntity_InsertsEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "Test Entity" };
        var validator = new TestEntityValidator();

        // Act
        var result = _baseService.Add<TestEntityValidator>(entity);

        // Assert
        _baseRepositoryMock.Verify(r => r.Insert(entity), Times.Once);
        Assert.Equal(entity, result);
    }

    [Fact]
    public void Add_InvalidEntity_ThrowsValidationException()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "" }; // Invalid entity (Name is empty)
        var validator = new TestEntityValidator();

        // Act & Assert
        Assert.Throws<ValidationException>(() => _baseService.Add<TestEntityValidator>(entity));
        _baseRepositoryMock.Verify(r => r.Insert(It.IsAny<TestEntity>()), Times.Never);
    }

    [Fact]
    public void Delete_ValidId_DeletesEntity()
    {
        // Arrange
        var id = 1;

        // Act
        _baseService.Delete(id);

        // Assert
        _baseRepositoryMock.Verify(r => r.Delete(id), Times.Once);
    }

    [Fact]
    public void Get_ReturnsAllEntities()
    {
        // Arrange
        var entities = new List<TestEntity> { new TestEntity { Id = 1, Name = "Test Entity" } };
        _baseRepositoryMock.Setup(r => r.Select()).Returns(entities);

        // Act
        var result = _baseService.Get();

        // Assert
        Assert.Equal(entities, result);
    }

    [Fact]
    public void GetById_ValidId_ReturnsEntity()
    {
        // Arrange
        var id = 1;
        var entity = new TestEntity { Id = id, Name = "Test Entity" };
        _baseRepositoryMock.Setup(r => r.Select(id)).Returns(entity);

        // Act
        var result = _baseService.GetById(id);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public void Update_ValidEntity_UpdatesEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "Updated Entity" };
        var validator = new TestEntityValidator();

        // Act
        var result = _baseService.Update<TestEntityValidator>(entity);

        // Assert
        _baseRepositoryMock.Verify(r => r.Update(entity), Times.Once);
        Assert.Equal(entity, result);
    }

    [Fact]
    public void Update_InvalidEntity_ThrowsValidationException()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "" }; // Invalid entity (Name is empty)
        var validator = new TestEntityValidator();

        // Act & Assert
        Assert.Throws<ValidationException>(() => _baseService.Update<TestEntityValidator>(entity));
        _baseRepositoryMock.Verify(r => r.Update(It.IsAny<TestEntity>()), Times.Never);
    }
}

public class TestEntity : BaseEntity
{
    public string Name { get; set; }
}

public class TestEntityValidator : AbstractValidator<TestEntity>
{
    public TestEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}
