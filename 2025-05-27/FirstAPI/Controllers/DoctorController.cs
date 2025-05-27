using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorController : ControllerBase
    {
        static List<Doctor> list = new List<Doctor>
        {
            new Doctor{ Id = 1, Name ="Hex"},
            new Doctor{ Id = 2, Name ="Hept"}
        };


        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
        {
            list.Add(doctor);
            return Created("", doctor);
        }

        [HttpDelete]
        public ActionResult DeleteDoctor(int id)
        {
            Doctor? del = list.Where(x => x.Id == id).FirstOrDefault();
            if (del != null)
            {
                list.Remove(del);
                return Ok(del);
            }
            return NotFound("No data Found");
        }

        [HttpPut]
        public ActionResult<Doctor> PutDoctor([FromBody] Doctor doctor)
        {
            Doctor? doc = list.Where(d => d.Id == doctor.Id).FirstOrDefault();
            if(doc == null) 
                return NotFound("No data Found");

            // int index = list.IndexOf(doc);
            // list.Remove(doc);
            // list.Insert(index, doctor);
            doc.Name = doctor.Name;
            return Ok(doctor);
            
        }
    }
}