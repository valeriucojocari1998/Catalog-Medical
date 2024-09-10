using Catalog_Medical.Models.Entities;
using System;

namespace Catalog_Medical.Models.Requests
{
    public class UpdatePatientRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Patient UpdatePatient(Patient patient)
        {
            patient.Name = this.Name;
            patient.DateOfBirth = this.DateOfBirth;
            patient.Age = DateTime.Now.Year - this.DateOfBirth.Year;
            patient.Gender = this.Gender;
            patient.PhoneNumber = this.PhoneNumber;
            patient.Email = this.Email;

            return patient;
        }
    }
}
