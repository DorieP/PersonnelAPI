using System;

namespace ApiBoilerPlate.Data.Entity
{
    public class Address : EntityBase
    {
        public long Id { get; set; }

        public long PersonnelId { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
