using LanceUpContactList.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LanceUpContactList.Helpers
{
	public static class JsonHelper
	{
		public static string Serializer(IEnumerable<Contact> contact)
		{
			string jsonString = string.Empty;
			jsonString = JsonConvert.SerializeObject(contact, Formatting.Indented);
			return jsonString;
		}

		public static IEnumerable<Contact> UnSerializer(string jsonString)
		{
			var contact = JsonConvert.DeserializeObject<IEnumerable<Contact>>(jsonString);

			return contact;
		}
	}
}
