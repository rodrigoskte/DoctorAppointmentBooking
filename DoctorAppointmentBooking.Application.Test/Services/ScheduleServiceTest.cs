using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Application.Services;

public class ScheduleServiceTest
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

    public ScheduleServiceTest()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
    }

    [Fact]
    public void GetAllWithDetails_ReturnsSchedules()
    {
        // Arrange
        var schedules = new List<Schedule> { new Schedule { Id = 1 } };
        _scheduleRepositoryMock.Setup(repo => repo.GetAllWithDetails()).Returns(schedules);

        // Act
        var result = _scheduleService.GetAllWithDetails();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(schedules.Count, result.Count);
    }

    [Fact]
    public void GetAllWithDetailsId_ReturnsSchedules()
    {
        // Arrange
        var id = 1;
        var schedules = new List<Schedule> { new Schedule { Id = 1 } };
        _scheduleRepositoryMock.Setup(repo => repo.GetAllWithDetailsId(id)).Returns(schedules);

        // Act
        var result = _scheduleService.GetAllWithDetailsId(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(schedules.Count, result.Count);
    }

    [Fact]
    public void Validations_ScheduleExists_ThrowsScheduleConflictException()
    {
        // Arrange
        var schedule = new Schedule
        {
            Id = 1,
            DoctorId = 1,
            PatientId = 1,
            DateTimeSchedule = DateTime.Now,
            Doctor = new Doctor { Name = "RC" },
            Patient = new Patient { Name = "CR" }
        };
        _scheduleRepositoryMock.Setup(repo => repo.IsScheduleExistsWithDocPatDat(schedule)).Returns(true);
        _scheduleRepositoryMock.Setup(repo => repo.GetScheduleWithDocPatDat(schedule)).Returns(schedule);

        // Act & Assert
        Assert.Throws<ScheduleConflictException>(() => _scheduleService.Validations(schedule));
    }

    [Fact]
    public void Validations_DoctorBusy_ThrowsScheduleDoctorConflictException()
    {
        // Arrange
        var schedule = new Schedule { Id = 1, DoctorId = 1, DateTimeSchedule = DateTime.Now, Doctor = new Doctor { Name = "RC" } };
        _scheduleRepositoryMock.Setup(repo => repo.IsScheduleExistsWithDocPatDat(schedule)).Returns(false);
        _scheduleRepositoryMock.Setup(repo => repo.IsDoctorBusy(schedule.DoctorId, schedule.DateTimeSchedule)).Returns(true);
        _scheduleRepositoryMock.Setup(repo => repo.GetDoctorBusy(schedule.DoctorId)).Returns(schedule);

        // Act & Assert
        var exception = Assert.Throws<ScheduleDoctorConflictException>(() => _scheduleService.Validations(schedule));
        Assert.NotNull(exception);
    }

    [Fact]
    public void Validations_InvalidScheduleTime_ThrowsInvalidScheduleTimeException()
    {
        // Arrange
        var schedule = new Schedule { Id = 1, DateTimeSchedule = DateTime.Now.AddDays(-1) };

        // Act & Assert
        var exception = Assert.Throws<InvalidScheduleTimeException>(() => _scheduleService.Validations(schedule));
        Assert.NotNull(exception);
    }

    [Fact]
    public void Validations_PatientNotAvailable_ThrowsPatientNotAvailableException()
    {
        // Arrange
        var schedule = new Schedule { Id = 1, PatientId = 1, DateTimeSchedule = DateTime.Now, Patient = new Patient { Name = "RC" }};
        _scheduleRepositoryMock.Setup(repo => repo.IsScheduleExistsWithDocPatDat(schedule)).Returns(false);
        _scheduleRepositoryMock.Setup(repo => repo.IsDoctorBusy(schedule.DoctorId, schedule.DateTimeSchedule)).Returns(false);
        _scheduleRepositoryMock.Setup(repo => repo.IsScheduleExistsWithPatient(schedule.PatientId, schedule.DateTimeSchedule)).Returns(true);
        _scheduleRepositoryMock.Setup(repo => repo.GetScheduleWithPatient(schedule.PatientId)).Returns(schedule);

        // Act & Assert
        var exception = Assert.Throws<PatientNotAvailableException>(() => _scheduleService.Validations(schedule));
    }

    [Fact]
    public void IsDoctorActiveSchedule_DoctorHasActiveSchedule_ThrowsDoctorScheduleActiveException()
    {
        // Arrange
        var doctorId = 1;
        var schedules = new List<Schedule> { new Schedule { Id = 1, DoctorId = doctorId } };
        _scheduleRepositoryMock.Setup(repo => repo.IsDoctorActiveSchedule(doctorId)).Returns(true);
        _scheduleRepositoryMock.Setup(repo => repo.GetAllScheduleDoctorId(doctorId)).Returns(schedules);

        // Act & Assert
        var exception = Assert.Throws<DoctorScheduleActiveException>(() => _scheduleService.IsDoctorActiveSchedule(doctorId));
        Assert.NotNull(exception);
    }

    [Fact]
    public void IsPatientActiveSchedule_PatientHasActiveSchedule_ThrowsPatientScheduleActiveException()
    {
        // Arrange
        var patientId = 1;
        var schedules = new List<Schedule> { new Schedule { Id = 1, PatientId = patientId } };
        _scheduleRepositoryMock.Setup(repo => repo.IsPatientActiveSchedule(patientId)).Returns(true);
        _scheduleRepositoryMock.Setup(repo => repo.GetAllSchedulePatientId(patientId)).Returns(schedules);

        // Act & Assert
        var exception = Assert.Throws<PatientScheduleActiveException>(() => _scheduleService.IsPatientActiveSchedule(patientId));
        Assert.NotNull(exception);
    }

    [Fact]
    public void GetAllSchedulePatientId_ReturnsSchedules()
    {
        // Arrange
        var patientId = 1;
        var schedules = new List<Schedule> { new Schedule { Id = 1, PatientId = patientId } };
        _scheduleRepositoryMock.Setup(repo => repo.GetAllSchedulePatientId(patientId)).Returns(schedules);

        // Act
        var result = _scheduleService.GetAllSchedulePatientId(patientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(schedules.Count, result.Count);
    }

    [Fact]
    public void GetAllScheduleDoctorId_ReturnsSchedules()
    {
        // Arrange
        var doctorId = 1;
        var schedules = new List<Schedule> { new Schedule { Id = 1, DoctorId = doctorId } };
        _scheduleRepositoryMock.Setup(repo => repo.GetAllScheduleDoctorId(doctorId)).Returns(schedules);

        // Act
        var result = _scheduleService.GetAllScheduleDoctorId(doctorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(schedules.Count, result.Count);
    }

    [Fact]
    public void CancelSchedulePatient_CancelsSchedules()
    {
        // Arrange
        var patientId = 1;

        // Act
        _scheduleService.CancelSchedulePatient(patientId);

        // Assert
        _scheduleRepositoryMock.Verify(repo => repo.CancelSchedulePatient(patientId), Times.Once);
    }

    [Fact]
    public void CancelScheduleDoctor_CancelsSchedules()
    {
        // Arrange
        var doctorId = 1;

        // Act
        _scheduleService.CancelScheduleDoctor(doctorId);

        // Assert
        _scheduleRepositoryMock.Verify(repo => repo.CancelScheduleDoctor(doctorId), Times.Once);
    }
}
