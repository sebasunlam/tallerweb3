using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;

namespace ProgramacionWeb3.Servicios.Contracts
{
    public interface IServicio<T> where T : Entidad
    {
        T Get(long id);
        T Get(string id);
        IList<T> GetAll();
        Page<T> GetAll<TKey>(int page, int pageSize, Expression<Func<T, TKey>> orderBy, bool ascending = true);
        Page<T> GetAll<TKey, TSec>(int page, int pageSize, Expression<Func<T, TKey>> orderBy, Expression<Func<T, TSec>> thenBy, bool ascending = true);
        IList<T> GetAllOrderedBy<TKey>(Expression<Func<T, TKey>> keySelector);

        void Create(T item, out long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        void DeleteChildren(long id);

        Page<T> GetPage<TKey>(int pageNumber,
                        int pageSize,
                              Expression<Func<T, TKey>> orderBy,
                              bool ascending = true,
                              Expression<Func<T, bool>> predicate = null);

        Page<T> GetPage<TKey, TSec>(int pageNumber,
                                    int pageSize,
                                    Expression<Func<T, TKey>> orderBy,
                                    Expression<Func<T, TSec>> thenBy,
                        bool ascending = true,
                        Expression<Func<T, bool>> predicate = null);

        Page<T> GetPage<TKey>(int pageNumber,
                              int pageSize,
                              IQueryable<T> query,
                              Expression<Func<T, TKey>> keySelector,
                              bool ascending = true);

        Page<T> GetPage<TKey, TSec>(int pageNumber,
                                      int pageSize,
                                      IQueryable<T> query,
                                      Expression<Func<T, TKey>> orderBy,
                                      Expression<Func<T, TSec>> thenBy,
                                      bool ascending = true);
    }
}