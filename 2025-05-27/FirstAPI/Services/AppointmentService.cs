// using FirstAPI.Interfaces;
// using FirstAPI.Models;
// using FirstAPI.Repositories;

// namespace FirstAPI.Services
// {
//     public class AppointmentService : IService<Appointment>
//     {
//         private readonly AppointmentRepository _appointmentRepository;
//         public AppointmentService(AppointmentRepository appointmentRepository)
//         {
//             _appointmentRepository = appointmentRepository;
//         }
//         public Appointment Add(Appointment item)
//         {
//             return _appointmentRepository.Add(item);
//         }

//         public IEnumerable<Appointment> Search(SearchModel searchModel)
//         {
//             IEnumerable<Appointment> appointments = _appointmentRepository.GetAll();
//             if (searchModel.AppointmentId != null)
//             {
//                 appointments = appointments.Where(a => a.AppointmentId == searchModel.AppointmentId);
//             }
//             if (searchModel.DoctorId != null)
//             {
//                 appointments = appointments.Where(a => a.DoctorId == searchModel.DoctorId);
//             }
//             if (searchModel.PatientId != null)
//             {
//                 appointments = appointments.Where(a => a.PatientId == searchModel.PatientId);
//             }
//             if (searchModel.AppointmentDate != null)
//             {
//                 appointments = appointments.Where(a => DateOnly.FromDateTime(a.AppointmentTime).Equals(DateOnly.FromDateTime((DateTime)searchModel.AppointmentDate)));
//             }
//             if (searchModel.Reason != null)
//             {
//                 appointments = appointments.Where((a) => a.Reason.Contains(searchModel.Reason));
//             }
//             return appointments;
//         }
//     }
// }