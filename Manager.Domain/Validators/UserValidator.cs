using FluentValidation;
using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidae não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                 .NotEmpty()
                 .WithMessage("O nome não pode ser vazio.")

                 .Length(3, 80)
                 .WithMessage("O nome dever conter entre 3 e 80 caracteres.");

            RuleFor(x => x.Email)
                 .NotNull()
                 .WithMessage("O email não pode ser nulo.")

                  .NotEmpty()
                  .WithMessage("O email não pode ser vazio.")

                 .EmailAddress()
                 .WithMessage("O email não é válido.");

            RuleFor(x => x.Password)
              .NotNull()
              .WithMessage("A senha não pode ser nula.")

               .NotEmpty()
               .WithMessage("A senha não pode ser vazia.")

              .MinimumLength(6)
                 .WithMessage("A senha dever conter no minímo 6 caracteres.");
        }
    }
}
