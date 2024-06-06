using Microsoft.Extensions.Caching.Memory;
using Fiap.Clientes.Domain.Clientes;
using Fiap.Clientes.Write.Database.Repositories.Abstractions;

namespace Fiap.Clientes.Write.Database.Repositories
{
    public class ClienteRepository : BaseRepository<ClienteEntity>, IClientRepository
    {

        public ClienteRepository(IMemoryCache cache): base(cache)
        {

        }

        public void Insert(ClienteEntity cliente)
        {
            Add(cliente,"CLIENTE");
        }

        public List<ClienteEntity> GetAll()
        {

            return GetAll("CLIENTE");
        }

        public ClienteEntity GetById(int id)
        {
            return GetAll("CLIENTE").Where(x => x.Id == id).FirstOrDefault();
        }

        public ClienteEntity GetByCPF(string cpf)
        {
            return GetAll("CLIENTE").Where(x => x.CPF == cpf).FirstOrDefault();
        }
    }
}
