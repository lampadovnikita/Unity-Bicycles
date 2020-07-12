using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidBehavior : MonoBehaviour
{
	public delegate void OnAsteroidDestroy();
	public OnAsteroidDestroy onAsteroidDestroyCallback;

	private Rigidbody2D asteroidRigidbody2D;

	private void Awake()
	{
		asteroidRigidbody2D = GetComponent<Rigidbody2D>();

		asteroidRigidbody2D.rotation = Random.Range(0f, 360f);

		asteroidRigidbody2D.AddRelativeForce(100f * Vector3.up);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (onAsteroidDestroyCallback != null)
		{
			onAsteroidDestroyCallback();
		}

		Destroy(gameObject);
	}

}
