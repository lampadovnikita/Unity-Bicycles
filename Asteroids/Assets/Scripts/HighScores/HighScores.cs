using UnityEngine;

public class HighScores : MonoBehaviour
{
	public static HighScores Instance { get; private set; }

	public HighScoresData ScoresData { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			ScoresData = new HighScoresData();
			LoadHighScores();
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	public void LoadHighScores()
	{
		HighScoresData loadedScoresData = HighScoresSerializer.Load();
		if (loadedScoresData != null)
		{
			ScoresData.ResetScore();

			int minLength = Mathf.Min(ScoresData.scores.Length, loadedScoresData.scores.Length);
			for (int i = 0; i < minLength; i++)
				ScoresData.AttemptAddScore(loadedScoresData.scores[i]);
		}
	}
}
