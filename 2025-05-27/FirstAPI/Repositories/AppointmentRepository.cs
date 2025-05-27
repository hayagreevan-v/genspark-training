// using FirstAPI.Interfaces;
// using FirstAPI.Models;

// namespace FirstAPI.Repositories
// {
//     public class AppointmentRepository : IRepository<int, Appointment>
//     {
//         static List<Appointment> _appointments = new List<Appointment>
//         {
//             new Appointment{AppointmentId=101, DoctorId= 1, PatientId=1, AppointmentTime= DateTime.Now, Reason= "Cold"},
//             new Appointment{AppointmentId=102, DoctorId= 1, PatientId=2, AppointmentTime= DateTime.Today, Reason= "Fever"}
//         };
//         public Appointment Add(Appointment newAppointment)
//         {
//             newAppointment.AppointmentId = GenerateID();
//             _appointments.Add(newAppointment);
//             return newAppointment;
//         }

//         public int GenerateID()
//         {
//             if (_appointments.Count == 0) return 101;
//             return (int)_appointments.Max(x => x.AppointmentId);
//         }

//         public Appointment? Get(int id)
//         {
//             return _appointments.FirstOrDefault(a => a.AppointmentId == id);
//         }

//         public IEnumerable<Appointment> GetAll()
//         {
//             return _appointments.ToList();
//         }
//     }
// }