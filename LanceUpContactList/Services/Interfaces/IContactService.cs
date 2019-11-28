using LanceUpContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanceUpContactList.Services.Interfaces
{
	public interface IContactService<T>
	{
		Task<bool> AddAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> DeleteAsync(string phone);
		Task<T> GetAsync(string phone);
		Task<IEnumerable<T>> ListAsync();
		Task<bool> SaveAsync(T entity);
	}
}
