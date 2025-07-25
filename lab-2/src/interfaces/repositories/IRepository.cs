using System;
using System.Collections.Generic;

namespace lab_2.interfaces.repositories;

public interface IRepository<T> where T : IEntity
{
    void Add(T entity);

    T GetById(Guid id);

    void Remove(Guid id);

    IEnumerable<T> GetAll();
}