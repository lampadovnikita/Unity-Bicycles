using System;
using System.Linq;
using UnityEngine;

public class HighScores
{
	public static readonly uint SCORES_LENGTH = 3;
	
	private static HighScores _instance;

	private int[] scores;

	public HighScores()
	{
		scores = new int[SCORES_LENGTH];
	}

	public HighScores(int[] inputScores)
	{
		scores = new int[SCORES_LENGTH];
		Array.Copy(inputScores.OrderByDescending(c => c).ToArray(), 0, scores, 0, SCORES_LENGTH);

		LogScores();
	}

	public static HighScores Instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}
			else
			{
				_instance = new HighScores();
				return _instance;
			}
		}
	}

	public int GetScore(int index)
	{
		return scores[index];
	}

	public void AddScore(int score)
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
