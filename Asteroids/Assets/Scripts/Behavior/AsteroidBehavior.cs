using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidBehavior : MonoBehaviour
{
	public delegate void AsteroidDestroy(int asteroidShardsNumber);
	public event AsteroidDestroy OnAsteroidDestroy;

	[SerializeField]
	private AudioClip destroyAudioClip = default;

	[SerializeField]
	private float forceScale = 500f;

	[SerializeField]
	private Pool shardPool = default;

	[SerializeField]
	private int shardsNumber = 3;

	private Rigidbody2D asteroidRigidbody2D;

	private void Awake()
	{
		asteroidRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioManager.Instance.EffectsAudioSource.PlayOneShot(destroyAudioClip);

		if (shardPool != null)
		{
			OnAsteroidDestroy?.Invoke(shardsNumber);

			GameObject shard;
			AsteroidBehavior shardAsteroidBehavior;

			// Random bias for asteroids shards make impossible to destroy all
			// newly spawned shards easily
			float randomCircleRadius = 2.5f;
			for (int i = 0; i < shardsNumber; i++)
			{
				shard = shardPool.GetObject();
				shard.transform.position = transform.position + 
					(Vector3)(Random.insideUnitCircle * randomCircleRadius);

				shardAsteroidBehavior = shard.GetComponent<AsteroidBehavior>();
				if (shardAsteroidBehavior != null)
				{
					shardAsteroidBehavior.AddRandomForceDirection();
					shardAsteroidBehavior.OnAsteroidDestroy += this.OnAsteroidDestroy;
				}
				else
				{
					Debug.LogWarning("Asteroid spawns shards without asteroid behavior component!");
				}
			}
		}
		else
		{
			OnAsteroidDestroy?.Invoke(0);
		}

		Hide();
	}

	private void Hide()
	{
		gameObject.SetActive(false);
		OnAsteroidDestroy = null;
	}

	public void AddRandomForceDirection()
	{
		float rotation = Random.Range(0f, 360f);

		asteroidRigidbody2D.SetRotation(rotation);
		gameObject.transform.eulerAngles = new Vector3(0f, 0f, rotation);

		asteroidRigidbody2D.AddRelativeForce(forceScale * Vector3.up);
	}
}
