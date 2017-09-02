using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using ProgramacionWeb3.Dominio.Contracts;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Extensions;
using ProgramacionWeb3.Servicios.Contracts;
using ProgramacionWeb3.Servicios.Exceptions;

namespace ProgramacionWeb3.Servicios.Implementation
{
    public class Servicio<T> : IServicio<T> where T : Entidad
    {
        #region Propiedades

        //protected readonly ILogger _logger;
        protected readonly IRepositorio<T> Repositorio;
        protected readonly IUnitOfWork UnitOfWork;

        #endregion

        #region constructor

        public Servicio(IRepositorio<T> repositorio, IUnitOfWork unitOfWork)
        {
            Repositorio = repositorio;
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region Metodos Publicos

        public T Get(long id)
        {
            var entity = Repositorio.Get(id);

            return entity;
        }

        public T Get(string id)
        {
            var entity = Repositorio.Get(id);
            return entity;
        }

		protected T Get(Expression<Func<T, bool>> predicate)
		{
			return GetAllItems().SingleOrDefault(predicate);
		}

		protected IQueryable<T> GetAllItems()
		{
			return GetAllItems(null);
		}

		protected IQueryable<T> GetAllItems(Expression<Func<T, bool>> predicate)
		{
			var query = Repositorio.GetAll();

            //todo: version que implementa IVisible para eliminar las referencias u objetos que tienen marca de no visibles
            //query = typeof(T).GetInterfaces().Contains(typeof(IVisible))
            //                                                ? query.Where(t => t.visible)
            //                                                : query;

			return predicate != null 
						? query.Where(predicate) 
						: query;
		}
		
        public IList<T> GetAll()
        {
	        return GetAllItems().ToList();
        }

        public IList<T> GetAllOrderedBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
	        return GetAllItems().OrderBy(keySelector).ToList();
        }

		public virtual Page<T> GetAll<TKey>(int page, int pageSize, Expression<Func<T, TKey>> orderBy, bool ascending = true)
		{
			return GetPage(page, pageSize, orderBy, ascending);
		}

		public virtual Page<T> GetAll<TKey, TSec>(int page, int pageSize, Expression<Func<T, TKey>> orderBy, Expression<Func<T, TSec>> thenBy, bool ascending = true)
		{
			return GetPage(page, pageSize, orderBy, thenBy, ascending);
		}

        public virtual void Create(T item, out long id)
        {
            Create(item);
            id = (long)item.GetType().GetProperty("Id").GetValue(item);
        }

        public virtual void Create(T item)
        {
            try
            {
                Repositorio.Add(item);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                //_logger.Debug(item.GetType() + " Error al crear:  " + ex.Message);
                throw new ServiceException("Ocurrio un error al crear el item", ex);
            }
        }

        public virtual void Update(T item)
        {
            try
            {
                Repositorio.Update(item);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                //_logger.Debug(item.GetType() + " Error al editar:  " + ex.Message);
                throw new ServiceException("Ocurrio un error al editar el item", ex);
            }
        }

		//protected virtual void Update(T item, Expression<Func<IUpdateConfiguration<T>, object>> mapping)
		//{
		//	try
		//	{
		//		Repositorio.Update(item, mapping);
		//		UnitOfWork.SaveChanges();
		//	}
		//	catch (Exception ex)
		//	{
		//		//_logger.Debug(item.GetType() + " Error al editar:  " + ex.Message);
		//		throw new ServiceException("Ocurrio un error al editar el item", ex);
		//	}
		//}

        public virtual void Delete(long id)
        {
            try
            {
                var item = Repositorio.Get(id);
                Repositorio.Delete(item);

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                //_logger.Debug(item.GetType() + " Error al eliminar:  " + ex.Message);
                throw new ServiceException("Ocurrio un error al eliminar el item", ex);
            }
        }

		public virtual void DeleteChildren(long id)
		{
			try
			{
				var item = Repositorio.Get(id);
				

				UnitOfWork.SaveChanges();
			}
			catch (Exception ex)
			{
				//_logger.Debug(item.GetType() + " Error al eliminar:  " + ex.Message);
				throw new ServiceException("Ocurrio un error al eliminar el item", ex);
			}
		}

		
        /// <summary>
        /// Función para obtener una página a partir del número y tamaño de la página deseada
        /// </summary>
        /// <param name="pageNumber">Número de página a devolver</param>
        /// <param name="pageSize">Tamaño deseado de la página</param>
        /// <param name="orderBy">Selector del campo de ordenamiento. Necesario debido al uso de la función Skip que requiere elementos ordenados.</param>
        /// <param name="ascending">Indicador para describir si el ordenamiento es ascendente o descendente</param>
        /// <param name="predicate">Predicado para realizar el filtrado de los elementos a devolver</param>
        /// <returns>Devuelve un objeto de tipo Page</returns>
        public Page<T> GetPage<TKey>(int pageNumber,
                                int pageSize,
                                Expression<Func<T, TKey>> orderBy,
                                bool ascending = true,
                                Expression<Func<T, bool>> predicate = null)
        {
            var query = GetAllItems().AsExpandable();

            if (predicate != null)
                query = query.Where(predicate);

	        return GetPage(pageNumber, pageSize, query, orderBy, ascending);
        }

		/// <summary>
		/// Función para obtener una página a partir del número y tamaño de la página deseada
		/// </summary>
		/// <param name="pageNumber">Número de página a devolver</param>
		/// <param name="pageSize">Tamaño deseado de la página</param>
		/// <param name="orderBy">Selector del campo de ordenamiento. Necesario debido al uso de la función Skip que requiere elementos ordenados.</param>
		/// <param name="thenBy">Selector secundario del campo de ordenamiento. Opcional.</param>
		/// <param name="ascending">Indicador para describir si el ordenamiento es ascendente o descendente</param>
		/// <param name="predicate">Predicado para realizar el filtrado de los elementos a devolver</param>
		/// <returns>Devuelve un objeto de tipo Page</returns>
		public Page<T> GetPage<TKey, TSec>(int pageNumber,
								int pageSize,
								Expression<Func<T, TKey>> orderBy,
								Expression<Func<T, TSec>> thenBy = null,
								bool ascending = true,
								Expression<Func<T, bool>> predicate = null)
		{
			var query = GetAllItems().AsExpandable();

			if (predicate != null)
				query = query.Where(predicate);

			return GetPage(pageNumber, pageSize, query, orderBy, thenBy, ascending);
        }

        /// <summary>
        /// Función para obtener una página a partir del número y tamaño de la página deseada
        /// </summary>
        /// <param name="pageNumber">Número de página a devolver</param>
        /// <param name="pageSize">Tamaño deseado de la página</param>
        /// <param name="orderBy">Selector del campo de ordenamiento. Necesario debido al uso de la función Skip que requiere elementos ordenados.</param>
        /// <param name="ascending">Indicador para describir si el ordenamiento es ascendente o descendente</param>
        /// <param name="query">Query para usar en lugar del repositorio por defecto</param>
        /// <returns>Devuelve un objeto de tipo Page</returns>
        public Page<T> GetPage<TKey>(int pageNumber,
                                int pageSize,
                                IQueryable<T> query,
                                Expression<Func<T, TKey>> orderBy,
                                bool ascending = true)
        {
			query = ascending 
							? query.OrderBy(orderBy) 
							: query.OrderByDescending(orderBy);

            return query.ToPage(pageNumber, pageSize);
        }

		/// <summary>
		/// Función para obtener una página a partir del número y tamaño de la página deseada
		/// </summary>
		/// <param name="pageNumber">Número de página a devolver</param>
		/// <param name="pageSize">Tamaño deseado de la página</param>
		/// <param name="orderBy">Selector del campo de ordenamiento. Necesario debido al uso de la función Skip que requiere elementos ordenados.</param>
		/// <param name="thenBy">Selector secundario del campo de ordenamiento. Opcional.</param>
		/// <param name="ascending">Indicador para describir si el ordenamiento es ascendente o descendente</param>
		/// <param name="query">Query para usar en lugar del repositorio por defecto</param>
		/// <returns>Devuelve un objeto de tipo Page</returns>
		public Page<T> GetPage<TKey, TSec>(int pageNumber,
											int pageSize,
											IQueryable<T> query,
											Expression<Func<T, TKey>> orderBy,
											Expression<Func<T, TSec>> thenBy,
											bool ascending = true)
		{
			query = ascending 
							? query.OrderBy(orderBy).ThenBy(thenBy) 
							: query.OrderByDescending(orderBy).ThenByDescending(thenBy);

			return query.ToPage(pageNumber, pageSize);
		}

        #endregion

    }
}
