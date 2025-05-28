using System;
using System.Security.Cryptography;
using FirstAPI.Imterfaces;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Speciality> _specialityRepository;
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;

    public DoctorService(
                            IRepository<int, Doctor> doctorRepository,
                            IRepository<int, Speciality> specialityRepository,
                            IRepository<int, DoctorSpeciality> doctorSpecialityRepository
                        )
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
        _doctorSpecialityRepository = doctorSpecialityRepository;
    }


    public async Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor)
    {
        Doctor newDoc = new Doctor
        {
            Name = doctor.Name,
            YearsOfExperience = doctor.YearsOfExperience,
            Status = "Created"
        };
        Doctor doc = await _doctorRepository.Add(newDoc);
        if (doctor.Specialities != null && doctor.Specialities.Count() > 0)
        {
            IEnumerable<Speciality> specialities = await _specialityRepository.GetAll();
            foreach (var speciality in doctor.Specialities)
            {
                Speciality? spec = specialities.FirstOrDefault(s => s.Name == speciality.Name);
                if (spec == null)
                {
                    spec = await _specialityRepository.Add(new Speciality { Name = speciality.Name, Status = "Created" });
                }
                await _doctorSpecialityRepository.Add(new DoctorSpeciality { DoctorId = doc.Id, SpecialityId = spec.Id });
            }
        }
        return doc;
    }

    public async Task<Doctor?> GetDoctorById(int id)
    {
        Doctor? doc = await _doctorRepository.Get(id);
        if (doc == null)
        {
            System.Console.WriteLine("NO doctor found");
            return null;
        }
        return doc;
    }
    
    public async Task<IEnumerable<Doctor>?> GetDoctorsByName(string name)
    {
        try
        {
            IEnumerable<Doctor> docList = await _doctorRepository.GetAll();
            docList = docList.Where(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (docList == null || docList.Count() == 0)
            {
                System.Console.WriteLine("No data found");
            }
            else
                return docList;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        return null;
    }

    public async Task<IEnumerable<Doctor>?> GetDoctorsBySpeciality(string speciality)
    {
        IEnumerable<Speciality> specs = await _specialityRepository.GetAll();
        Speciality? spec = specs.FirstOrDefault(d => d.Name == speciality);
        if (spec == null)
        {
            System.Console.WriteLine("No specified speciality exists");
            return null;
        }
        IEnumerable<DoctorSpeciality> docspecs = await _doctorSpecialityRepository.GetAll();
        docspecs = docspecs.Where(d => d.SpecialityId == spec.Id);
        if (docspecs == null || docspecs.Count() == 0)
        {
            System.Console.WriteLine("No doctor with specified speciality exists");
            return null;
        }
        IEnumerable<int> docids = docspecs.Select(d => d.DoctorId).Distinct();
        IEnumerable<Doctor> docs= await _doctorRepository.GetAll();
        docs = docs.Where(d => docids.Contains(d.Id) );
        return docs;

    }
}
