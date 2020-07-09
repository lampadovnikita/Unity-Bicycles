using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	[SerializeField]
	private Player player = default;

	[SerializeField]
	private int playerOriginLifes = 1;

	[SerializeField]
	private AsteroidBehavior asteroidPrefab = default;

	[SerializeField]
	private int asteroidsSpawnLimit = 10;

	[SerializeField, Range(0.0001f, 0.5f)]
	private float xSpawnPaddingFactor = 0.1f;

	[SerializeField, Range(0.0001f, 0.5f)]
	private float ySpawnPaddingFactor = 0.1f;

	[SerializeField]
	private GameObject gameOverUI = default;

	[SerializeField]
	private TextMeshProUGUI scoreTextMeshPro = default;

	private int score = 0;

	private int playerLifes = default;

	private int activeAsteroidsCount = 0;

	private float xSpawnPadding = default;
	private float ySpawnPadding = default;

	private Vector3 topRightBound = default;
	private Vector3 bottomLeftBound = default;

	private void Start()
	{
		float zDistance = transform.position.z - Camera.main.transform.position.z;

		bottomLeftBound = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDistance));
		topRightBound = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, zDistance));

		xSpawnPadding = xSpawnPaddingFactor * Mathf.Abs(topRightBound.x - bottomLeftBound.x);
		ySpawnPadding = ySpawnPaddingFactor * Mathf.Abs(topRightBound.y - bottomLeftBound.y);

		player.onPlayerDestroyCallback += OnPlayerDestroy;

		playerLifes = playerOriginLifes;

		scoreTextMeshPro.text = score.ToString();

		SpawnAsteroids();
	}

	private void Update()
	{
		if (player.isActiveAndEnabled == true)
		{
			if (Input.GetAxisRaw("Vertical") > 0)
			{
				player.AddRelativeForce(Player.ForceDirection.Up);
			}
			if (Input.GetAxisRaw("Horizontal") > 0)
			{
				player.AddRotation(Player.RotationDirection.Right);
			}
			if (Input.GetAxisRaw("Vertical") < 0)
			{
				player.AddRelativeForce(Player.ForceDirection.Down);
			}
			if (Input.GetAxisRaw("Horizontal") < 0)
			{
				player.AddRotation(Player.RotationDirection.Left);
			}
			if (Input.GetButtonDown("Fire1"))
			{
				player.GetComponent<ShootBehavior>().Shoot();
			}
		}
	}

	private void SpawnAsteroids()
	{
		Vector3 spawnPosition = new Vector3();
		spawnPosition.z = transform.position.z;

		AsteroidBehavior asteroid;

		float xIndent;
		float yIndent;

		for (int i = 0; i < asteroidsSpawnLimit; i++)
		{
			// Choose the side along to which we wil generate a position.
			// The generated position locates near chosen axis with some padding,
			// so there some area around the border to spawn objects.
			// if the RNG produce 0, generate the position along the y axis;
			// else (if the RNG produce 1), generate the position along the x axis.
			if (Random.Range(0, 2) == 0)
			{
				xIndent = Random.Range(-1 * xSpawnPadding, xSpawnPadding);
				if (xIndent > 0)
				{
					spawnPosition.x = bottomLeftBound.x + xIndent;
				}
				else
				{
					// The indent is negative, so the addition move the objects position to the left
					// of the screen.
					spawnPosition.x = topRightBound.x + xIndent;
				}

				spawnPosition.y = Random.Range(bottomLeftBound.y, topRightBound.y);
			}
			else
			{
				yIndent = Random.Range(-1 * ySpawnPadding, ySpawnPadding);
				if (yIndent > 0)
				{
					spawnPosition.y = bottomLeftBound.y + yIndent;
				}
				else
				{
					// The indent is negative, so the addition move the objects position to the bottom
					// of the screen.
					spawnPosition.y = topRightBound.y + yIndent;
				}

				spawnPosition.x = Random.Range(bottomLeftBound.x, topRightBound.x);
			}

			asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
			asteroid.onAsteroidDestroyCallback = OnAsteroidDestroy;

			activeAsteroidsCount++;
		}
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
			SpawnAsteroids();
		}
	}
}
