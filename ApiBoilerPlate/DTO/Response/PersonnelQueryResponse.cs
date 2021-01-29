using ApiBoilerPlate.Data.Entity;
using System;

namespace ApiBoilerPlate.DTO.Response
{
    public class PersonnelQueryResponse
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public EmployeeType Type { get; set; }
    }
}
