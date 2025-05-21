using Questions_PatientManagementSystem.Repositories;
using Questions_PatientManagementSystem.Services;

namespace Questions_PatientManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientRepository patientRepository = new PatientRepository();
            AppointmentService appointmentService = new AppointmentService(patientRepository);
            ManageService manageService = new ManageService(appointmentService);
            manageService.Start();
        }
        
    }
}