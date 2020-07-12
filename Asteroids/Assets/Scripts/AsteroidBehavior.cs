using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidBehavior : MonoBehaviour
{
	public delegate void OnAsteroidDestroy();
	public OnAsteroidDestroy onAsteroidDestroyCallback;

	[SerializeField]
	private AudioClip destroyAudioClip = default;

	[SerializeField]
	private float forceScale = 300f;

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

		if (onAsteroidDestroyCallback != null)
		{
			onAsteroidDestroyCallback();
		}

		Destroy(gameObject);
	}

}
