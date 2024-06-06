using AutoMapper;
using Moq;
using Fiap.Clientes.Domain.Clientes;
using FluentAssertions;
using Fiap.Clientes.Shared.Exceptions;
using System.Collections.Generic;
using FluentAssertions.Equivalency;

namespace Fiap.Clientes.Unit.Tests.Factories
{
    public class ClienteFactoryTest
    {
        public IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteEntity, ClienteInput>().ReverseMap();
            });
            return mockMapper.CreateMapper();
        }
        private Mock<IClientRepository> GetClienteRepository()
        {
            Mock<IClientRepository> clienteRepository = new();
            clienteRepository.Setup(c => c.GetById(123));
            clienteRepository.Setup(c => c.GetByCPF("97230782015"));

            return clienteRepository;
        }
        private ClienteInput GetClienteInput() 
        {
            return  new()
            {
                Id = 123,
                Nome = "Tatiana Lima",
                CPF = "97230782015",
                Email = "tatidornel@gmail.com",
                Telefone = "(11) 974025555"
            };
        }

        [Theory(DisplayName = "Validar se o nome está vazio")]
        [InlineData("")]
        [InlineData(null)]        
        [Trait("Action", "Create")]
        public void Create_EmptyNome_ShouldThrowReembolsoException(string nome)
        {
            ClienteInput input = GetClienteInput();
            input.Nome = nome;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() =>  factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O campo nome é obrigatório.");

        }
        
        [Theory(DisplayName = "Validar se o nome ultrapassou 200 caracteres")]
        [InlineData("Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT Tatiana Maria          XPOT")]
        [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567892")]
        
        [Trait("Action", "Create")]
        public void Create_ExceedingMaximumNome_ShouldThrowReembolsoException(string nome)
        {
            ClienteInput input = GetClienteInput();
            input.Nome = nome;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("A nome deve conter no máxim 200 caracteres.");

        }

        [Theory(DisplayName = "Validar se o email está vazio")]
        [InlineData("")]
        [InlineData(null)]

        [Trait("Action", "Create")]
        public void Create_EmptyEmail_ShouldThrowReembolsoException(string email)
        {
            ClienteInput input = GetClienteInput();
            input.Email = email;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O campo email é obrigatório.");

        }

        [Theory(DisplayName = "Validar se o email ultrapassou o máximo de caracteres")]

        [InlineData("tatianalimatatianalimatatianalimatatianalimatatianalimatatianalimatatianalimatatianalimatatianalimatatianalima@teste.com")]
        [InlineData("a123456789a123456789a123456789a123456789a123456789a123456789a123456789a123456789a123456789a123456789@g.com")]
        [Trait("Action", "Create")]
        public void Create_ExceedingMaximumEmail_ShouldThrowReembolsoException(string email)
        {
            ClienteInput input = GetClienteInput();
            input.Email = email;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
          
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O e-mail deve conter no máxim 100 caracteres.");

        }

        [Theory(DisplayName = "Validar se o telefone está vazio")]
        [InlineData("")]
        [InlineData(null)]

        [Trait("Action", "Create")]
        public void Create_EmptyTelefone_ShouldThrowReembolsoException(string telefone)
        {
            ClienteInput input = GetClienteInput();
            input.Telefone = telefone;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O campo telefone é obrigatório.");

        }

        [Theory(DisplayName = "Validar se o telefone ultrapassou o máximo de caracteres")]
        [InlineData("(12) 123456789000")]
        [InlineData("(11) 9999999-999")]
        [Trait("Action", "Create")]
        public void Create_ExceedingMaximumTelefone_ShouldThrowReembolsoException(string telefone)
        {
            ClienteInput input = GetClienteInput();
            input.Telefone = telefone;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O e-mail deve conter no máxim 15 caracteres.");

        }

        [Theory(DisplayName = "Validar se o CPF está vazio")]
        [InlineData("")]
        [InlineData(null)]

        [Trait("Action", "Create")]
        public void Create_EmptyCPF_ShouldThrowReembolsoException(string cpf)
        {
            ClienteInput input = GetClienteInput();
            input.CPF = cpf;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O campo CPF é obrigatório.");

        }

        [Theory(DisplayName = "Validar se o cpf ultrapassou o máximo de caracteres")]
        [InlineData("999999999999")]
        [InlineData("989.987.999-23")]
        [Trait("Action", "Create")]
        public void Create_ExceedingMaximumCPF_ShouldThrowReembolsoException(string cpf)
        {
            ClienteInput input = GetClienteInput();
            input.CPF = cpf;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("O cpf deve conter no máximo 11 caracteres.");

        }

        [Theory(DisplayName = "Validar se o email é inválido")]
        [InlineData("@g")]
        [InlineData("t@g")]
        [InlineData("t@g.")]
        [InlineData("tatiana&*@gmail.com")]
        [InlineData("tatiana.com")]
        [InlineData("tatiana")]
        [Trait("Action", "Create")]
        public void Create_InvalidEmail_ShouldThrowReembolsoException(string email)
        {
            ClienteInput input = GetClienteInput();
            input.Email = email;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("Email inválido.");

        }

        [Theory(DisplayName = "Validar se o telefone é inválido")]
        [InlineData("+55119999999")]
        [InlineData("(11) 99999999")]
        [InlineData("1199999999")]
        [InlineData("tatiana")]
        [Trait("Action", "Create")]
        public void Create_InvalidTelefone_ShouldThrowReembolsoException(string telefone)
        {
            ClienteInput input = GetClienteInput();
            input.Telefone = telefone;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("Telefone inválido.");

        }

        [Theory(DisplayName = "Validar se o cpf é inválido")]
        [InlineData("11111111111")]
        [InlineData("00000000000")]
        [InlineData("123123.12-2")]
        [InlineData("A7230782015")]
        [Trait("Action", "Create")]
        public void Create_InvalidCPF_ShouldThrowReembolsoException(string cpf)
        {
            ClienteInput input = GetClienteInput();
            input.CPF = cpf;

            ClienteFactory factory = new(GetMapper(), GetClienteRepository().Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("CPF inválido.");

        }

        [Theory(DisplayName = "Validar se o código do cliente já foi cadastrado")]
        [InlineData(121)]
        [Trait("Action", "Create")]
        public void Create_RegisteredId_ShouldThrowReembolsoException(int id)
        {
            ClienteInput input = GetClienteInput();
            input.Id = id;

            var retCliente = new ClienteEntity() { Id = id  };

            var  clienteRepository = new Mock<IClientRepository>();
            clienteRepository.Setup(c => c.GetByCPF(input.CPF)).Returns(retCliente);
            clienteRepository.Setup(c => c.GetById(input.Id)).Returns(retCliente);
            

            ClienteFactory factory = new(GetMapper(), clienteRepository.Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("Código do cliente já cadastrado.");

        }
        [Theory(DisplayName = "Validar se o cpf do cliente já foi cadastrado")]
        [InlineData(121)]
        [Trait("Action", "Create")]
        public void Create_RegisteredCPF_ShouldThrowReembolsoException(int id)
        {
            ClienteInput input = GetClienteInput();
            input.Id = id;

            Mock<IClientRepository> clienteRepository = new();
            clienteRepository.Setup(c => c.GetById(input.Id));
            clienteRepository.Setup(c => c.GetByCPF(input.CPF)).Returns(new ClienteEntity() { Id = id });

            ClienteFactory factory = new(GetMapper(), clienteRepository.Object);
            var exception = Assert.Throws<FiapException>(() => factory.Create(input));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("CPF do cliente já cadastrado.");

        }
    }
}