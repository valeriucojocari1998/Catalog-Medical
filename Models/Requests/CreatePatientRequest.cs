using Catalog_Medical.Models.Entities;
using System;

namespace Catalog_Medical.Models.Requests
{
    public class CreatePatientRequest
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Patient ToPatient(string doctorId)
        {
            return new Patient
            {
                Id = Guid.NewGuid().ToString(""),
                Name = this.Name,
                DoctorId = doctorId,
                DateOfBirth = this.DateOfBirth,
                Age = DateTime.Now.Year - this.DateOfBirth.Year,
                Gender = this.Gender,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
            };
        }
    }
}
