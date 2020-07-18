using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidBehavior : MonoBehaviour
{
	public delegate void AsteroidDestroy(int asteroidShardsNumber);
	public event AsteroidDestroy OnAsteroidDestroy;

	[SerializeField]
	private AudioClip destroyAudioClip = default;

	[SerializeField]
	private float forceScale = 300f;

	[SerializeField]
	private AsteroidBehavior shardPrefab = default;

	[SerializeField]
	private int shardsNumber = 3;

	private Rigidbody2D asteroidRigidbody2D;
	
	private void Awake()
	{
		asteroidRigidbody2D = GetComponent<Rigidbody2D>();

		asteroidRigidbody2D.rotation = Random.Range(0f, 360f);

		asteroidRigidbody2D.AddRelativeForce(forceScale * Vector3.up);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioManager.Instance.GlobalAudioSource.PlayOneShot(destroyAudioClip);

		if (shardPrefab != null)
		{
			OnAsteroidDestroy?.Invoke(shardsNumber);
		}
		else
		{
			OnAsteroidDestroy?.Invoke(0);
		}

		AsteroidBehavior shard;
		if (shardPrefab != null)
		{
			for (int i = 0; i < shardsNumber; i++)
			{
				shard = Instantiate(shardPrefab);
				shard.transform.position = transform.position;
				shard.OnAsteroidDestroy += this.OnAsteroidDestroy;
			}
		}

		Destroy(gameObject);
	}

}
