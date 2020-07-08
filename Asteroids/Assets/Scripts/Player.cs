using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _playerRigidbody2D;

	private float _maxAxisVelocity = 42f;

	[SerializeField]
	private float mainForce = 1000f;

	[SerializeField]
	private float rotationForce = 200f;

	private void Awake()
	{
		_playerRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameObject.SetActive(false);
	}

	public void AddRelativeForce(ForceDirection forceDirection)
	{
		_playerRigidbody2D.AddRelativeForce(mainForce * Time.deltaTime * forceDirection.GetForce());

		if (Mathf.Abs(_playerRigidbody2D.velocity.x) > _maxAxisVelocity)
		{
			float newVelocity = (_playerRigidbody2D.velocity.x > 0) ? _maxAxisVelocity : -1 * _maxAxisVelocity;
			_playerRigidbody2D.velocity = new Vector2(newVelocity, _playerRigidbody2D.velocity.y);
		}

		if (Mathf.Abs(_playerRigidbody2D.velocity.y) > _maxAxisVelocity)
		{
			float newVelocity = (_playerRigidbody2D.velocity.y > 0) ? _maxAxisVelocity : -1 * _maxAxisVelocity;
			_playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, newVelocity);
		}
	}

	public void AddRotation(RotationDirection rotationDirection)
	{
		_playerRigidbody2D.rotation += rotationForce * Time.deltaTime * rotationDirection.GetRoataion();
	}
}
