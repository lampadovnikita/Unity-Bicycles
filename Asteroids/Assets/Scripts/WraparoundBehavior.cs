using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WraparoundBehavior : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer spriteRenderer = default;

	private float upperBound;
	private float rightBound;
	private float bottomBound;
	private float leftBound;

	private Rigidbody2D wrappedRigidbody2D;

	private void Awake()
	{
		wrappedRigidbody2D = transform.GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		float zDistance = transform.position.z - Camera.main.transform.position.z;

		Vector3 _bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDistance));
		Vector3 _topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, zDistance));

		float _spriteWidth = spriteRenderer.bounds.size.x * 0.5f;
		float _spriteHeight = spriteRenderer.bounds.size.y * 0.5f;

		rightBound = _topRight.x + _spriteWidth;
		leftBound = _bottomLeft.x - _spriteWidth;

		upperBound = _topRight.y + _spriteHeight;
		bottomBound = _bottomLeft.y - _spriteHeight;
	}

	private void LateUpdate()
	{
		Vector3 position = transform.position;

		// Position should be swapped to a bound coordinate instead of inverted position of an object
		// because that approach prevent bug with endless position inversion of an object that out of
		// the screen area.
		if (position.x > rightBound)
		{
			wrappedRigidbody2D.position = new Vector2(leftBound, position.y);
		}
		else if (position.x < leftBound)
		{
			wrappedRigidbody2D.position = new Vector2(rightBound, position.y);
		}
		else if (position.y > upperBound)
		{
			wrappedRigidbody2D.position = new Vector2(position.x, bottomBound);
		}
		else if (position.y < bottomBound)
		{
			wrappedRigidbody2D.position = new Vector2(position.x, upperBound);
		}
	}
}
