using System;

namespace Questions_PatientManagementSystem.Models;

public class SearchModel
{
    public string? PatientName { get; set; }
    public DateOnly? AppointmentDate { get; set; }
    public Range<int>? AgeRange { get; set; }

    public override string ToString()
    {
        if (AgeRange != null)
            return $"Name : {PatientName}\nDate : {AppointmentDate}\nAge : {AgeRange.MinVal} {AgeRange.MaxVal}";
        return $"Name : {PatientName}\nDate : {AppointmentDate}\nAge : {null}";
    }
}

public class Range<T>
{
    public T? MinVal{ get; set; }
    public T? MaxVal{ get; set; }
}