using FluentValidation;
using System.Text.RegularExpressions;
using Fiap.Clientes.Shared.Abstractions.Entities;
using Fiap.Clientes.Shared.Extensions;

namespace Fiap.Clientes.Domain.Clientes
{
    public class ClienteEntity : EntityBase
    {
        public ClienteEntity() { 
            DataCriacao = DateTime.Now;
        }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }

        public string CPF { get; private set; }

        public void Update(int id, string nome, string email, string telefone, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            CPF = cpf;
        }
        public override bool Validate()
        {
            var validator = new ClienteValidator();
            var validation = validator.Validate(this);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);
                return false;
            }
            return true;
        }

        internal class ClienteValidator : AbstractValidator<ClienteEntity>
        {

            public ClienteValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                        .WithMessage("O campo nome é obrigatório.");

                RuleFor(x => x.Nome)
                    .MaximumLength(200)
                        .WithMessage("A nome deve conter no máxim 200 caracteres.");

                RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("O campo email é obrigatório.")

                  .MaximumLength(100)
                        .WithMessage("O e-mail deve conter no máxim 100 caracteres.");

                RuleFor(x => x.Telefone)
                    .NotEmpty()
                        .WithMessage("O campo telefone é obrigatório.")

                    .MaximumLength(15)
                        .WithMessage("O e-mail deve conter no máxim 15 caracteres.");

                RuleFor(x => x.CPF)
                    .NotEmpty()
                        .WithMessage("O campo CPF é obrigatório.")

                    .MaximumLength(11)
                        .WithMessage("O cpf deve conter no máximo 11 caracteres.");

                RuleFor(x => new { x.Email, x.CPF, x.Telefone}).Custom((value, context) =>
                {
                    if (!value.Email.IsValidEmail())
                        context.AddFailure("Email inválido.");
                    if (!value.Telefone.IsValidTelefone())
                        context.AddFailure("Telefone inválido.");
                    if (!value.CPF.IsCPFValid())
                        context.AddFailure("CPF inválido.");
                });
            }

            

            

            
        }
    }
}
