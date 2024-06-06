using Microsoft.Extensions.Caching.Memory;
using Fiap.Clientes.Shared.Abstractions.Entities;

namespace Fiap.Clientes.Write.Database.Repositories.Abstractions
{
    public abstract class BaseRepository<T> where T : EntityBase
    {
        private readonly IMemoryCache _cache;

        public BaseRepository(IMemoryCache cache)
        {
            _cache = cache;

        }

        public virtual void Add(T entity, string key)
        {
            List<T> clientes = GetAll(key);
            if (clientes == null) clientes = new List<T>();
            clientes.Add(entity);
            
            MemoryCacheEntryOptions opt = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                
            };
            opt.SetPriority(CacheItemPriority.High);
            _cache?.Set(key, clientes, opt);
        }

        public virtual List<T> GetAll(string key)
        {
            _cache.TryGetValue(key, out object? value);
            if (value != null)
            {
                return (List<T>)value;
            }

            return new List<T>();
        }
    }
}
