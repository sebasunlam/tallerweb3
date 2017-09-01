using System;
using System.Linq;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;

namespace ProgramacionWeb3.Dominio.Contracts
{
	public interface IRepositorio<T> where T: Entidad
	{
		T Get(long id);
		T Get(string id);
		//IList<T> GetAll();
		IQueryable<T> GetAll();
		Page<T> GetPage(int pageNumber, int pageSize, string sortBy, bool ascending, string searchCriteria);
		void Add(T entity);
		void Update(T entity);
		//void Update(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping);
		void Delete(T entity);
		void Delete(Func<T, bool> predicate);
	}
}
