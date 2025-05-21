using System;
using Questions_PatientManagementSystem.Interfaces;
using Questions_PatientManagementSystem.Models;
using Questions_PatientManagementSystem.Repositories;

namespace Questions_PatientManagementSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly PatientRepository _patientrepository;

    public AppointmentService(PatientRepository patientRepository)
    {
        _patientrepository = patientRepository;
    }

    public int AddAppointment(Appointment appointment)
    {
        try
        {
            Appointment app = _patientrepository.Add(appointment);
            return app.Id;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return -1;
    }

    public ICollection<Appointment>? searchAppointment(SearchModel searchModel)
    {
        try
        {
            ICollection<Appointment> appointments = _patientrepository.GetAll();
            if (appointments == null || appointments.Count() == 0)
            {
                System.Console.WriteLine("No appointments found");
                return null;
            }
            appointments = SearchByName(appointments, searchModel.PatientName);
            appointments = SearchByAge(appointments, searchModel.AgeRange);
            appointments = SearchByDate(appointments, searchModel.AppointmentDate);
            return appointments.ToList();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return null;
    }

    private ICollection<Appointment> SearchByName( ICollection<Appointment> appointments, string? name)
    {
        if (string.IsNullOrEmpty(name)) return appointments;
        return appointments.Where(a => a.PatientName.ToLower().Contains(name.ToLower())).ToList();
    }
    private ICollection<Appointment> SearchByAge(ICollection<Appointment> appointments, Range<int>? age)
    {
        if (age == null) return appointments;
        return appointments.Where(a => a.PatientAge>=age.MinVal && a.PatientAge<= age.MaxVal).ToList();
    }
    private ICollection<Appointment> SearchByDate(ICollection<Appointment> appointments, DateOnly? date)
    {
        if (date == null) return appointments;
        return appointments.Where(a => DateOnly.FromDateTime(a.AppointmentDate).Equals(date)).ToList();
    }

}
