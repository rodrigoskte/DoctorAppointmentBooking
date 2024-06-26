﻿using DoctorAppointmentBooking.Application.Constants;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ISpecialtyRepository _specialtyRepository;

    public DoctorService(
        IDoctorRepository doctorRepository,
        ISpecialtyRepository specialtyRepository)
    {
        _doctorRepository = doctorRepository;
        _specialtyRepository = specialtyRepository;
    }

    public IList<Doctor> GetDoctorsWithSpecialties()
    {
        return _doctorRepository.GetDoctorsWithSpecialties();
    }

    public IList<Doctor> GetDoctorsWithSpecialtiesId(int id)
    {
        return _doctorRepository.GetDoctorsWithSpecialtiesId(id);
    }
    
    public void AddDoctor(Doctor doctor)
    {
        var specialtiesExist = doctor.DoctorSpecialties
            .All(ds => _specialtyRepository.Select(ds.SpecialtyId) != null);

        if (!specialtiesExist)
            throw new ArgumentException(MessageConstants.SpecialtyNotFound);

        _doctorRepository.AddDoctor(doctor);
    }

    public bool Validations(Doctor doctor)
    {
        if (_doctorRepository.IsDoctorExists(doctor))
            throw new DoctorException(doctor);

        return false;
    }

    public IList<Doctor> GetAllDoctorsActive()
    {
        return _doctorRepository.GetAllDoctorsActive();
    }
    
    public IList<Doctor> GetAllDoctorsActiveById(int doctorId)
    {
        return _doctorRepository.GetAllDoctorsActiveById(doctorId);
    }

    public Doctor GetDoctorByUserId(string userId)
    {
        return _doctorRepository.GetDoctorByUserId(userId);
    }
}