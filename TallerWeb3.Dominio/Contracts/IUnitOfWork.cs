namespace ProgramacionWeb3.Dominio.Contracts
{
	public interface IUnitOfWork
	{
		int SaveChanges();
		int SaveChanges(bool validateOnSaveEnabled);
	}
}
