using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PayCore_H2.Controllers;

namespace PayCore_H2
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(s => s.Name).Matches(@"^[a-zA-Z/]*$").NotEmpty().Length(5,120);

            RuleFor(s => s.LastName).Matches(@"^[a-zA-Z/]*$").NotEmpty().Length(5,120);

            RuleFor(s => s.Salary).GreaterThan(2000).LessThan(9000);

            RuleFor(s => s.DateOfBirth.Date).NotEmpty()
               .GreaterThan(new DateTime(1945, 11, 11))
               .LessThan(new DateTime(2002, 10, 10));

            RuleFor(s=>s.Email).EmailAddress().Matches(@"^[a-zA-Z/(.@)]*$");

            RuleFor(s=>s.PhoneNumber).Matches(@"^([+](\d{2})(\d{3})(\d{3})(\d{2})(\d{2}))$");



        }
    }
}
