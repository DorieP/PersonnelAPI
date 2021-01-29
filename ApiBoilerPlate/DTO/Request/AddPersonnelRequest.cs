using ApiBoilerPlate.Data.Entity;
using FluentValidation;
using System;

namespace ApiBoilerPlate.DTO.Request
{
    public class AddPersonnelRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EmployeeType Type { get; set; }
        public Payroll Pay { get; set; }
        public Address Address { get; set; }
    }

    public class CreatePersonnelRequestValidator : AbstractValidator<AddPersonnelRequest>
    {
        public CreatePersonnelRequestValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.DateOfBirth).NotEmpty();
            RuleFor(o => o.Type).NotEmpty();
        }
    }
}
