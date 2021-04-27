using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Domain
{
    public interface IReporistory
    {
        void Add<T>(T entity) where T : BaseEntity;
        bool TryGet<T>(Guid id, out T entity) where T : BaseEntity;
        List<T> GetAll<T>() where T : BaseEntity;
    }

    public class Repository : IReporistory
    {
        private readonly MemoryCache _cache;
        private readonly List<string> _cacheKeys;

        public Repository()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cacheKeys = new List<string>();
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            var cacheKey = GetCacheKey(entity);
            _cache.Set(cacheKey, entity);
            _cacheKeys.Add(cacheKey);
        }

        public bool TryGet<T>(Guid id, out T entity) where T : BaseEntity
        {
            var cacheKey = GetCacheKey(typeof(T).Name, id);
            entity = _cache.Get<T>(cacheKey);
            
            return entity != null;
        }

        public List<T> GetAll<T>() where T : BaseEntity
        {
            var entityName = typeof(T).Name;
            var entityCacheKeys = _cacheKeys.Where(item => item.StartsWith(entityName));

            var result = new List<T>();

            foreach (var entityCacheKey in entityCacheKeys)
            {
                var entity = Get<T>(entityCacheKey);
                result.Add(entity);
            }

            return result;
        }

        public T Get<T>(string key) where T : BaseEntity
        {
            return _cache.Get<T>(key);
        }

        private string GetCacheKey<T>(T entity) where T : BaseEntity
        {
            var entityName = entity.GetType().Name;

            return GetCacheKey(entityName, entity.Id);
        }

        private string GetCacheKey(string entityName, Guid entityId)
        {
            var cacheKey = $"{entityName}_{entityId}";

            return cacheKey;
        }
    }
}
