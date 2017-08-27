using System;
using System.Linq;
using TallerWeb3.Dominio.Contracts;
using TallerWeb3.Dominio.Extensions;

namespace TallerWeb3.Dominio.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : Entidad
    {
        protected readonly DbContext DbContext;

        public Repositorio(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual T Get(long id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual T Get(string id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            //var adapter = (IObjectContextAdapter)_dbContext;
            //var objectContext = adapter.ObjectContext;
            //objectContext.CommandTimeout = 5 * 60; // value in seconds
            return DbContext.Set<T>().Select(x => x);
        }

        public virtual void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            var id = entity.GetType().GetProperty("Id").GetValue(entity);
            var currentEntity = DbContext.Set<T>().Find(id);
            DbContext.Entry(currentEntity).CurrentValues.SetValues(entity);
        }

		//public virtual void Update(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping)
		//{
		//	Update(entity);
		//	DbContext.UpdateGraph(entity, mapping);
		//}

        public virtual void Delete(T entity)
        {
			if (DbContext.Entry(entity).State == EntityState.Detached)
                DbContext.Set<T>().Attach(entity);
            
            DbContext.Set<T>().Remove(entity);
        }
		
        public virtual void Delete(Func<T, bool> predicate)
        {
            var records = DbContext.Set<T>().Where(predicate);

			foreach (var record in records)
			{
				DbContext.Set<T>().Remove(record);
			}
                
        }

        public Page<T> GetPage(int pageNumber, int pageSize, string sortBy, bool ascending, string searchCriteria)
        {
            var query = DbContext.Set<T>().Select(x => x);
            return query.ToPage(pageNumber, pageSize);
        }
    }
}
