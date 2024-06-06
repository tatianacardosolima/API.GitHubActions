using Fiap.Clientes.Shared.Abstractions.Entities;

namespace Fiap.Clientes.Shared.Exceptions
{
    public class FiapException: Exception
    {
        public FiapException(string message) : base(message) { }
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
                throw new FiapException(message);
        }

        public static void ThrowWhenInvalidEntity(EntityBase entity)
        {
            if (entity.Validate())
                return;

            throw new FiapException(entity.Errors.ElementAt(0));
        }
    }
}
