using System;
using Questions_PatientManagementSystem.Models;
using Questions_PatientManagementSystem.Services;

namespace Questions_PatientManagementSystem;

public class ManageService
{
    AppointmentService _appointmentService;

    public ManageService(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    void AddAppointment()
    {
        Appointment appointment = new Appointment();
        appointment.GetData();
        int id = _appointmentService.AddAppointment(appointment);
        Console.WriteLine($"New Record inserted at ID : {id}");

    }
    SearchModel GetSearchModel()
    {
        string? s, s2;
        DateOnly date;
        int minAge, maxAge;
        SearchModel searchModel = new SearchModel();

        Console.Write("Enter Search Name [or press Enter] : ");
        s = Console.ReadLine();
        if (!string.IsNullOrEmpty(s)) searchModel.PatientName = s;

        Console.Write("Enter Search Date [yyyy-mm-dd] : ");
        s = Console.ReadLine();
        if (!string.IsNullOrEmpty(s) && DateOnly.TryParse(s, out date))
            searchModel.AppointmentDate = date;

        Console.WriteLine("Enter Search Age Range :");
        Console.Write("Min Age : ");
        s = Console.ReadLine();
        Console.Write("Max Age : ");
        s2 = Console.ReadLine();
        if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(s2) && int.TryParse(s, out minAge) && int.TryParse(s2, out maxAge))
        {
            searchModel.AgeRange = new Range<int>();
            searchModel.AgeRange.MinVal = minAge;
            searchModel.AgeRange.MaxVal = maxAge;
        }

        return searchModel;
    }
    void SearchAppointment()
    {
        SearchModel searchModel = GetSearchModel();

        // Console.WriteLine("Search Model : " + searchModel+"\n");       
        ICollection<Appointment>? appointments = _appointmentService.searchAppointment(searchModel) ;
        if (appointments == null || appointments.Count() == 0)
        {
            // System.Console.WriteLine("No data found");
            return;
        }
        System.Console.WriteLine();
        System.Console.WriteLine("-------------Patients------------");
        foreach (Appointment a in appointments.ToList())
        {
            Console.WriteLine(a);   
            Console.WriteLine();   
        }
    }
    public void Start()
    {
        int choice = 0;
        while (choice <= 3)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add Appointment");
            Console.WriteLine("2. Search Appointment");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice : ");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Invalid Input!");
                Console.WriteLine("Enter your choice : ");
            }
            switch (choice)
            {
                case 1:
                    AddAppointment();
                    break;
                case 2:
                    SearchAppointment();
                    break;
                case 3:
                    System.Console.WriteLine("Exiting...");
                    return;
                default:
                    System.Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }
}
