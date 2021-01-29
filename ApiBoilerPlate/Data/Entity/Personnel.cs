using System;

namespace ApiBoilerPlate.Data.Entity
{
    public class Personnel : EntityBase
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        // This is an Enum, in the db will have a constraint to be only the enum values 
        public EmployeeType Type { get; set; }
    }
}
