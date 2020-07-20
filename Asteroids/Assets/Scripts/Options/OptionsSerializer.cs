using System.IO;
using UnityEngine;

public static class OptionsSerializer
{
	private static readonly string SAVE_FILE_NAME = "options.txt";

	private static readonly string SAVE_FILE_ABSOLUTE_PATH = Application.persistentDataPath + "/" + SAVE_FILE_NAME;
	
	public static void Save(OptionsData data)
	{
		string jsonString = JsonUtility.ToJson(data);

		File.WriteAllText(SAVE_FILE_ABSOLUTE_PATH, jsonString);
	}

	public static OptionsData Load()
	{
		if (File.Exists(SAVE_FILE_ABSOLUTE_PATH) == true)
		{
			string jsonString = File.ReadAllText(SAVE_FILE_ABSOLUTE_PATH);
			
			OptionsData data = JsonUtility.FromJson<OptionsData>(jsonString);
			
			return data;
		}
		else 
		{
			return null;
		}
	}
}
