using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	public enum ForceDirection
	{
		Up,
		Down
	}

	public enum RotationDirection
	{
		Right,
		Left
	}

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
		Vector2 forceDirectionVector = Vector2.zero;
		switch (forceDirection)
		{
			case ForceDirection.Up:
				forceDirectionVector = Vector2.up;
				break;
			case ForceDirection.Down:
				forceDirectionVector = Vector2.down;
				break;
			default:
				Debug.Assert(false, "Unprocessed force direction case!");
				break;
		}

		_playerRigidbody2D.AddRelativeForce(mainForce * Time.deltaTime * forceDirectionVector);

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
		float rotationDirectionValue = 0f;
		switch (rotationDirection)
		{
			case RotationDirection.Right:
				rotationDirectionValue = -1f;
				break;
			case RotationDirection.Left:
				rotationDirectionValue = 1f;
				break;
			default:
				Debug.Assert(false, "Unprocessed rotation direction case!");
				break;
		}

		_playerRigidbody2D.rotation += rotationForce * Time.deltaTime * rotationDirectionValue;
	}
}
