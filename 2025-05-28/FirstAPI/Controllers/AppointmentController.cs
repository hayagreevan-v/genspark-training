// using FirstAPI.Models;
// using FirstAPI.Repositories;
// using FirstAPI.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace FirstAPI.Controllers
// {
//     [ApiController]
//     [Route("/api/[controller]")]
//     public class AppointmentController : ControllerBase
//     {
//         static AppointmentService appointmentService = new AppointmentService(new AppointmentRepository());

//         [HttpPost]
//         public ActionResult Add(Appointment appointment)
//         {
//             try
//             {
//                 Appointment a = appointmentService.Add(appointment);
//                 return Created("", a);
//             }
//             catch (Exception ex)
//             {
//                 return NotFound(ex);
//             }
//         }
//     }
// }