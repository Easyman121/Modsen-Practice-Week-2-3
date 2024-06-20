﻿using FluentValidation;
using DataAccessLayer.Models;
using System;

namespace BusinessLogicLayer.Validators
{
    public class OrderValidator : AbstractValidator<Orders>
    {
        public OrderValidator()
        {
            RuleFor(o => o.DateTime)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} cannot be in the future")
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-20)).WithMessage("{PropertyName} cannot be earlier than 20 years ago");

            RuleFor(o => o.User)
                .NotNull().WithMessage("User is required.")
                .SetValidator(new UserValidator());

            RuleForEach(o => o.OrderItems)
                .SetValidator(new OrderItemValidator());
        }
    }
}