using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public enum GameState
	{
		Active,
		Pause
	}

	private static GameState _curentGameState;
	public static GameState CurrentGameState
	{
		get
		{
			return _curentGameState;
		}
		private set
		{
			_curentGameState = value;
		}
	}

	[SerializeField]
	private Player player = default;

	[SerializeField]
	private int playerOriginLifes = 1;

	[SerializeField]
	private AsteroidSpawner asteroidSpawner = default;

	[SerializeField]
	private GameObject gameOverUI = default;

	[SerializeField]
	private GameObject pauseUI = default;

	[SerializeField]
	private TextMeshProUGUI scoreTextMeshPro = default;

	private int score = 0;

	private int playerLifes = default;

	private int activeAsteroidsCount = 0;

	private bool isGamePaused = false;

	private void Start()
	{
		player.onPlayerDestroyCallback += OnPlayerDestroy;

		playerLifes = playerOriginLifes;

		scoreTextMeshPro.text = score.ToString();

		activeAsteroidsCount = asteroidSpawner.SpawnAsteroids(OnAsteroidDestroy);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (isGamePaused == true)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}

	public void PauseGame()
	{
		CurrentGameState = GameState.Pause;
		Time.timeScale = 0f;
		pauseUI.SetActive(true);
	}

	public void ResumeGame()
	{
		CurrentGameState = GameState.Active;
		Time.timeScale = 1f;
		pauseUI.SetActive(false);
	}

	private void OnPlayerDestroy()
	{
		playerLifes--;

		if (playerLifes == 0)
		{
			gameOverUI.SetActive(true);
		}
	}

	private void OnAsteroidDestroy()
	{
		activeAsteroidsCount--;

		score += 50;
		scoreTextMeshPro.text = score.ToString();

		if (activeAsteroidsCount == 0)
		{
			activeAsteroidsCount = asteroidSpawner.SpawnAsteroids(OnAsteroidDestroy);
		}
	}
}
