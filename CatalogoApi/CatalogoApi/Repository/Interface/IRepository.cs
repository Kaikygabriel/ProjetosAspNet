﻿using System.Linq.Expressions;

namespace CatalogoApi.Repository.Interface
{
    public interface IRepository<T> where T:class 
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T,bool>>predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
