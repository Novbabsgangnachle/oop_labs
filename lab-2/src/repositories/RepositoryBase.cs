using System;
using System.Collections.Generic;
using lab_2.interfaces;
using lab_2.interfaces.repositories;

namespace lab_2.repositories;

public class RepositoryBase<T> : IRepository<T> where T : IEntity
{
    private static RepositoryBase<T>? _instance;

    public static RepositoryBase<T> Instance()
    {
        if (_instance == null)
        {
            _instance = new RepositoryBase<T>();
        }

        return _instance;
    }

    private readonly Dictionary<Guid, T> _storage = new();

    private RepositoryBase() { }

    public void Add(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (_storage.ContainsKey(entity.Id))
            throw new ArgumentException($"{typeof(T).Name} with the same ID already exists.");

        _storage[entity.Id] = entity;
    }

    public T GetById(Guid id)
    {
        if (!_storage.TryGetValue(id, out T? entity))
            throw new KeyNotFoundException($"{typeof(T).Name} not found.");

        return entity;
    }

    public void Remove(Guid id)
    {
        if (!_storage.ContainsKey(id))
            throw new KeyNotFoundException($"{typeof(T).Name} not found.");

        _storage.Remove(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _storage.Values;
    }
}