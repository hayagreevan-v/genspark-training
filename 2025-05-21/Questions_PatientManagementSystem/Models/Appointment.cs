using System;

namespace Questions_PatientManagementSystem.Models;

public class Appointment : IComparable<Appointment>, IEquatable<Appointment>
{
    public int Id { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int PatientAge { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    public int CompareTo(Appointment? other)
    {
        return this.Id.CompareTo(other?.Id);
    }

    public bool Equals(Appointment? other)
    {
        return this.Id.Equals(other?.Id);
    }

    public override string ToString()
    {
        return $"Id : {Id}\nPatientName : {PatientName}\nPatientAge : {PatientAge}\nAppointmentDate : {AppointmentDate}\nReason : {Reason}\n";
    }

    public void GetData()
    {
        string? s;
        int age;
        DateTime date;

        // Console.Write("Enter Patient ID : ");
        // Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Patient Name : ");
        PatientName = Console.ReadLine()!;

        Console.Write("Enter Patient Age : ");
        while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
        {
            Console.WriteLine("Invalid Input!");
            Console.Write("Enter Patient Age : ");
        }
        PatientAge = age;

        Console.Write("Enter Appointment Date [yyyy-mm-dd] : ");
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Invalid Input!");
            Console.Write("Enter Appointment Date [yyyy-mm-dd] : ");
        }
        AppointmentDate = date;
        
        Console.Write("Enter Reason : ");
        s = Console.ReadLine();
        while (string.IsNullOrEmpty(s))
        {
            Console.WriteLine("Invalid Input!");
            Console.Write("Enter Reason : ");
            s = Console.ReadLine();
        }
        Reason = s;
    }
}
