using AutoMapper;
using FluentAssertions;
using Moq;
using Fiap.Clientes.Domain.Clientes;
using Fiap.Clientes.Shared.Exceptions;

namespace Fiap.Clientes.Unit.Tests.Services
{
    public class ClienteServiceTest
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

        private Mock<IClienteFactory> GetClienteFactory(ClienteInput input)
        {
            Mock<IClienteFactory> factory = new();
            var entity = new ClienteEntity();
            entity.Update(input.Id, input.Nome, input.Email, input.Telefone, input.CPF);
            factory.Setup(c => c.Create(input)).Returns(entity);
            return factory;
        }

        private ClienteInput GetClienteInput()
        {
            return new()
            {
                Id = 123,
                Nome = "Tatiana Lima",
                CPF = "97230782015",
                Email = "tatidornel@gmail.com",
                Telefone = "(11) 974025555"
            };
        }


        [Theory(DisplayName = "Salvar um novo cliente")]
        [InlineData(121)]
        [InlineData(122)]
        [InlineData(123)]
        [Trait("Action", "Create")]
        public void Create_RegisteredSuccess_ShouldThrowReembolsoException(int id)
        {
            ClienteInput input = GetClienteInput();
            input.Id = id;

            var retCliente = new ClienteEntity() { Id = id };

            var clienteRepository = new Mock<IClientRepository>();
            clienteRepository.Setup(c => c.GetByCPF(input.CPF)).Returns(retCliente);
            clienteRepository.Setup(c => c.GetById(input.Id)).Returns(retCliente);


            ClienteService service = new(GetClienteFactory(input).Object, GetClienteRepository().Object);
            var response = service.Save(input);
            response.Sucesso.Should().BeTrue();
            

        }
    }
}