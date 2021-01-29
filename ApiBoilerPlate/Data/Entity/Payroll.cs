using System;

namespace ApiBoilerPlate.Data.Entity
{
    public class Payroll : EntityBase
    {
        public long Id { get; set; }

        public long PersonnelId { get; set; }

        // This will be the rate per hour or the annual salary determined by the PayrollType
        public float Rate { get; set; }

        // This is an Enum, in the db will have a constraint to be only the enum values 
        public PayrollType Type { get; set; }
    }
}
