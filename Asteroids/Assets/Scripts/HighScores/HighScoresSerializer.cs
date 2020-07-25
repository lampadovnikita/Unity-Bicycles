using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class HighScoresSerializer
{
	private static readonly string SAVE_FILE_NAME = "hs.byc";

	private static readonly string SAVE_FILE_ABSOLUTE_PATH = Application.persistentDataPath + "/" + SAVE_FILE_NAME;

	public static void Save(HighScoresData scoreData)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(SAVE_FILE_ABSOLUTE_PATH, FileMode.Create);

		formatter.Serialize(stream, scoreData);
		stream.Close();
	}

	public static HighScoresData Load()
	{
		if (File.Exists(SAVE_FILE_ABSOLUTE_PATH))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(SAVE_FILE_ABSOLUTE_PATH, FileMode.Open);

			HighScoresData scoreData = formatter.Deserialize(stream) as HighScoresData;
			stream.Close();

			return scoreData;
		}
		else 
		{
			Debug.Log("High scores save file: " + SAVE_FILE_ABSOLUTE_PATH + "  not found!");
			return null;
		}
	}
}
