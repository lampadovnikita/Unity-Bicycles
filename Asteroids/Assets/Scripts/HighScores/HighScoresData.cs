using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class HighScoresData
{
	private const int SCORES_LENGTH = 5;

	public readonly int[] scores;

	public HighScoresData()
	{
		scores = new int[SCORES_LENGTH];
	}

	public void AttemptAddScore(int score)
	{
		int insertionIndex = -1;
		for (int i = 0; i < scores.Length; i++)
		{
			if (scores[i] < score)
			{
				insertionIndex = i;
				break;
			}
		}

		if (insertionIndex != -1)
		{
			for (int i = scores.Length - 2; i >= insertionIndex; i--)
			{
				scores[i + 1] = scores[i];
			}

			scores[insertionIndex] = score;
		}

		LogScores();
	}

	public void ResetScore()
	{
		Array.Clear(scores, 0, scores.Length);
	}

	private void LogScores()
	{

		String logString = "Score array contains: [";
		foreach (int score in scores)
		{
			logString += score + ", ";
		}
		logString = logString.Remove(logString.Length - 2);
		logString += "]";

		Debug.Log(logString);
	}
}
