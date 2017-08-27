using System;

namespace TallerWeb3.Servicios.Exceptions
{
	public class ServiceException : Exception
	{
		public ServiceException(string message)
			: base(message)
		{
		}

		public ServiceException(string message, Exception e)
			: base(message, e)
		{
		}
	}
}
