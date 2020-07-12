using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField]
	private AsteroidBehavior asteroidPrefab = default;

	[SerializeField]
	private int asteroidsSpawnLimit = 10;

	[SerializeField, Range(0.0001f, 0.5f)]
	private float xSpawnPaddingFactor = 0.1f;

	[SerializeField, Range(0.0001f, 0.5f)]
	private float ySpawnPaddingFactor = 0.1f;

	private float xSpawnPadding;
	private float ySpawnPadding;

	private Vector3 topRightBound;
	private Vector3 bottomLeftBound;

	private void Awake()
	{
		float zDistance = transform.position.z - Camera.main.transform.position.z;

		bottomLeftBound = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDistance));
		topRightBound = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, zDistance));

		xSpawnPadding = xSpawnPaddingFactor * Mathf.Abs(topRightBound.x - bottomLeftBound.x);
		ySpawnPadding = ySpawnPaddingFactor * Mathf.Abs(topRightBound.y - bottomLeftBound.y);
	}

	public int SpawnAsteroids(AsteroidBehavior.OnAsteroidDestroy onAsteroidDestroy)
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

			if (onAsteroidDestroy != null)
			{ 
				asteroid.onAsteroidDestroyCallback += onAsteroidDestroy;			
			}
		}

		return asteroidsSpawnLimit;
	}
}
