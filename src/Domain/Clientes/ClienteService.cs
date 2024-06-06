using Fiap.Clientes.Shared.Outputs;

namespace Fiap.Clientes.Domain.Clientes
{
    public interface IClienteService
    {
        GenericOutput Save(ClienteInput input);
    }
    public class ClienteService : IClienteService
    {
        private readonly IClienteFactory _factory;
        private readonly IClientRepository _repository;

        public ClienteService(IClienteFactory factory, IClientRepository repository)
        {
            _factory = factory;
            _repository = repository;
        }
        public GenericOutput Save(ClienteInput input)
        {
           
            var entity = _factory.Create(input);
           
           _repository.Insert(entity);

            return new GenericOutput(true, "Cliente inserido com sucesso", entity.Id);

        }
    }
}
