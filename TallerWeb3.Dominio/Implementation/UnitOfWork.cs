using System.Data.Entity;
using ProgramacionWeb3.Dominio.Contracts;

namespace ProgramacionWeb3.Dominio.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public int SaveChanges(bool validateOnSaveEnabled)
        {
            _dbContext.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;

            return _dbContext.SaveChanges();
        }
    }
}