using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public enum GameState
	{
		Active,
		Pause
	}

	public static GameState CurrentGameState { get; private set; } = GameState.Active;

	[SerializeField]
	private List<Pool> pools = default;

	[SerializeField]
	private GameObject pregeneratedContainer = default;

	[SerializeField]
	private Player player = default;

	[SerializeField]
	private int playerOriginLifes = 1;

	[SerializeField]
	private List<Image> lifeCountImages = default;

	[SerializeField]
	private float playerRespawnDelay = 1f; // In seconds

	[SerializeField]
	private AsteroidSpawner asteroidSpawner = default;

	[SerializeField]
	private GameObject gameOverUI = default;

	[SerializeField]
	private GameObject pauseUI = default;

	[SerializeField]
	private TextMeshProUGUI scoreTextMeshPro = default;

	private int score;

	private int currentPlayerLifes;

	private int activeAsteroidsCount;

	private void Awake()
	{
		// There should be a call of this method because if the user starts the game
		// after returning to the main menu, then the game state is still paused and
		// the time is still frozen.
		ResumeGame();

		score = 0;

		currentPlayerLifes = playerOriginLifes;

		activeAsteroidsCount = 0;

		foreach (Pool pool in pools)
		{
			pool.Initialize(pregeneratedContainer);
		}
	}

	private void Start()
	{
		player.OnPlayerDestroy += OnPlayerDestroy;

		scoreTextMeshPro.text = score.ToString();

		activeAsteroidsCount = asteroidSpawner.SpawnAsteroids(OnAsteroidDestroy);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (CurrentGameState == GameState.Pause)
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
		currentPlayerLifes--;

		lifeCountImages[playerOriginLifes - currentPlayerLifes - 1].enabled = false;

		if (currentPlayerLifes == 0)
		{
			gameOverUI.SetActive(true);
		}
		else 
		{
			StartCoroutine(RespawnPlayer());
		}
	}

	private IEnumerator RespawnPlayer()
	{
		yield return new WaitForSeconds(playerRespawnDelay);

		player.Respawn();
	}

	private void OnAsteroidDestroy(int asteroidShardsNumber)
	{
		activeAsteroidsCount--;
		activeAsteroidsCount += asteroidShardsNumber;

		score += 50;
		scoreTextMeshPro.text = score.ToString();

		if (activeAsteroidsCount == 0)
		{
			activeAsteroidsCount = asteroidSpawner.SpawnAsteroids(OnAsteroidDestroy);
		}
	}
}
