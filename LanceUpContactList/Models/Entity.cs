using System;

namespace LanceUpContactList.Models
{
	public abstract class Entity
	{
		public Guid Id { get; set; }
		public DateTime RegistrationDate { get; set; }
		protected Entity()
		{
			Id = Guid.NewGuid();
			RegistrationDate = DateTime.Now;
		}
	}
}
