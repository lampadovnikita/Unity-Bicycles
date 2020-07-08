using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour
{
	private Rigidbody2D _bulletRigidBody2D = default;

	private float _lifeTimeStorage = 0f;

	[SerializeField]
	private float force = 1500f;

	[SerializeField]
	private float maxLifeTime = 2f;

	private void Start()
	{
		_bulletRigidBody2D = GetComponent<Rigidbody2D>();

		_bulletRigidBody2D.AddRelativeForce(force * ForceDirectionExtensions.GetForce(ForceDirection.Up));

	}

	private void Update()
	{
		_lifeTimeStorage += Time.deltaTime;

		if (_lifeTimeStorage > maxLifeTime)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
