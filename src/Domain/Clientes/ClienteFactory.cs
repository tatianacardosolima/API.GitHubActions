using AutoMapper;
using Fiap.Clientes.Shared.Exceptions;

namespace Fiap.Clientes.Domain.Clientes
{
    public interface IClienteFactory
    {
        ClienteEntity Create(ClienteInput input);
    }
    public class ClienteFactory : IClienteFactory
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClienteFactory(IMapper mapper, IClientRepository clientRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }
        public ClienteEntity Create(ClienteInput input)
        {
            var entity = _mapper.Map<ClienteEntity>(input);
            
            FiapException.ThrowWhenInvalidEntity(entity);

            FiapException.ThrowWhen(_clientRepository.GetById(entity.Id) != null,
                                    "Código do cliente já cadastrado.");

            FiapException.ThrowWhen(_clientRepository.GetByCPF(entity.CPF) != null,
                                    "CPF do cliente já cadastrado.");

            return entity;
        }
    }
}
