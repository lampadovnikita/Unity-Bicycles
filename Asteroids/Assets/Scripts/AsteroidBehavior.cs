using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidBehavior : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D = default;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();

		_rigidbody2D.rotation = Random.Range(0f, 360f);

		_rigidbody2D.AddRelativeForce(100f * Vector3.up);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}

}
