using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour
{
	private Rigidbody2D bulletRigidBody2D = default;

	private float lifeTimeStorage = 0f;

	[SerializeField]
	private float force = 1500f;

	[SerializeField]
	private float maxLifeTime = 2f;

	private void Start()
	{
		bulletRigidBody2D = GetComponent<Rigidbody2D>();

		bulletRigidBody2D.AddRelativeForce(force * Vector2.up);

	}

	private void Update()
	{
		lifeTimeStorage += Time.deltaTime;

		if (lifeTimeStorage > maxLifeTime)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
