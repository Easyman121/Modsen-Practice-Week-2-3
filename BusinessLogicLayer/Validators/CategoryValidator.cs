using FluentValidation;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Validators
{
    public class CategoryValidator : AbstractValidator<Categories>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50).WithMessage("The length of {PropertyName} must be between 2 and 50 characters. The current length is {TotalLength}");
        }
    }
}

