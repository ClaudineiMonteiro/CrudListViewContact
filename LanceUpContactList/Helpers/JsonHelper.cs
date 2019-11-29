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
		public static string Serializer<T>(T obj)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			MemoryStream ms = new MemoryStream();
			serializer.WriteObject(ms, obj);
			return Encoding.UTF8.GetString(ms.ToArray());
		}

		public static IEnumerable<T> UnSerializer<T>(string jsonString)
		{
			var teste = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);

			return teste;
		}
	}
}
