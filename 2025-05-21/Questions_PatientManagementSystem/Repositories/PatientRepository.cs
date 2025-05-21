using System;
using Questions_PatientManagementSystem.Interfaces;
using Questions_PatientManagementSystem.Models;

namespace Questions_PatientManagementSystem.Repositories;

public class PatientRepository : IRepository<int, Appointment>
{
    List<Appointment> list = new List<Appointment>();
    public Appointment Add(Appointment patient)
    {
        if (patient == null || list.Contains(patient))
        {
            System.Console.WriteLine("Invalid Input");
            throw new Exception("Invalid Input");
        }
        patient.Id = GenerateID();
        list.Add(patient);
        return patient;
    }

    public ICollection<Appointment> GetAll()
    {
        if (list.Count() == 0)
        {
            throw new KeyNotFoundException("No data found");
        }
        return list;
    }

    public Appointment GetById(int id)
    {
        Appointment? appointment = list.FirstOrDefault(p => p.Id == id);
        if (appointment == null)
        {
            throw new KeyNotFoundException("No Data Found");
        }
        return appointment;
    }

    public int GenerateID()
    {
        if (list.Count() == 0) return 101;
        return list.Max(l => l.Id) + 1;

    }
}
