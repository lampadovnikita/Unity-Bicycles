using System.IO;
using UnityEngine;

namespace TowerBlocks
{
	// Work of this class can be optimized if cache read files
	public static class SaveLoader
	{
		private const string SAVE_FILE_LOCAL_PATH = "/save.json";

		private static string SaveFileAbsolutePath
		{
			get
			{
				return Application.persistentDataPath + SAVE_FILE_LOCAL_PATH;
			}
		}

		public static void Save(SaveData data)
		{
			string strData = JsonUtility.ToJson(data);

			File.WriteAllText(SaveFileAbsolutePath, strData);
		}

		public static SaveData Load()
		{
			SaveData data;
			try
			{ 
				string strData = File.ReadAllText(SaveFileAbsolutePath);
				data = JsonUtility.FromJson<SaveData>(strData);
			}
			catch (FileNotFoundException)
			{
				data = new SaveData();
			}

			return data;
		}
	}
}