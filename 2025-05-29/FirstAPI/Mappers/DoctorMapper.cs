using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Mappers
{
    public class DoctorMapper
    {
        public static Doctor MapDoctorAddRequestDoctor(DoctorAddRequestDTO addRequestDTO)
        {
            Doctor doctor = new Doctor
            {
                Name = addRequestDTO.Name,
                YearsOfExperience = addRequestDTO.YearsOfExperience,
                Status = "Created"
            };
            return doctor;
        }
    }
}