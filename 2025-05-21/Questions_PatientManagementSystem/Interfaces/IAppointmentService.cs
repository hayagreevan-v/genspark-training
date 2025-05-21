using System;
using Questions_PatientManagementSystem.Models;

namespace Questions_PatientManagementSystem.Interfaces;

public interface IAppointmentService
{
    int AddAppointment(Appointment appointment);
    ICollection<Appointment>? searchAppointment(SearchModel searchModel);
}
