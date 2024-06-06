namespace Fiap.Clientes.Domain.Clientes
{
    public interface IClientRepository
    {
        void Insert(ClienteEntity cliente);

        List<ClienteEntity> GetAll();

        ClienteEntity GetById(int id);

        ClienteEntity GetByCPF(string cpf);
    }
}
