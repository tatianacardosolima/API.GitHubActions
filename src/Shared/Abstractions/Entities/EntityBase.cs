using Fiap.Clientes.Shared.Interfaces.IEntities;

namespace Fiap.Clientes.Shared.Abstractions.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get ; set ; }

        protected List<string> _errors = new List<string>();
        public IReadOnlyCollection<string> Errors => _errors;

        public abstract bool Validate();
    }
}
