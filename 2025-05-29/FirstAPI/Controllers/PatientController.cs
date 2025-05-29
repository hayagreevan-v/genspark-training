using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        static List<Patient> patients = new List<Patient>
        {
            new Patient { Id = 1, Name = "Qwerty", Age= 68},
            new Patient { Id = 2, Name = "Asdf", Age= 74},
            new Patient { Id = 1, Name = "Bob", Age= 89}
        };


        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            Patient? p = patients.FirstOrDefault(p => p.Id == id);
            if (p == null)
                return NotFound("No data found");
            return Ok(p);
        }


        [HttpPost]
        public ActionResult<Patient> AddPatient(Patient patient)
        {
            patients.Add(patient);
            return Created("", patient);
        }


        [HttpPut]
        public ActionResult<Patient> UpdatePatient([FromBody] Patient patient)
        {
            Patient? p = patients.Where(p => p.Id == patient.Id).FirstOrDefault();
            if (p == null)
            {
                return NotFound("No data found");
            }
            p.Name = patient.Name;
            p.Age = patient.Age;
            return Ok(p);
        }


        [HttpDelete("{id}")]
        public ActionResult<Patient> DeletePatient(int id)
        {
            Patient? p = patients.Where(p => p.Id == id).FirstOrDefault();
            if (p == null) return NotFound("Nd data found");

            patients.Remove(p);
            return Ok(p);
        }
    }
}