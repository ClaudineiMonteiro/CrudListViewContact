using LanceUpContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanceUpContactList.Services.Interfaces
{
	public interface IContactService
	{
		Task<bool> AddAsync(Contact contact);
		Task<bool> UpdateAsync(Contact Contact);
		Task<bool> DeleteAsync(string phone);
		Task<Contact> GetAsync(string phone);
		Task<IEnumerable<Contact>> ListAsync(bool forceRefresh = false);
	}
}
