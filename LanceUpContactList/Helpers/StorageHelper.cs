using System;
using System.IO;
using System.Threading.Tasks;

namespace LanceUpContactList.Helpers
{
	public static class StorageHelper
	{
		public static void WriteFile(string fileName, string writestr)
		{
			var localPath = Path.Combine(App.FolderPath, fileName);

			if (File.Exists(localPath))
			{
				File.Delete(localPath);
			}

			File.WriteAllText(localPath, writestr);
		}

		public static string ReadFile(string fileName)
		{
			string jsonString = string.Empty;
			var localPath = Path.Combine(App.FolderPath, fileName);

			if (localPath == null || !File.Exists(localPath))
			{
				return jsonString;
			}

			using (var reader = new StreamReader(localPath))
			{
				jsonString = reader.ReadToEnd();
			}

			return jsonString;
		}
	}
}
