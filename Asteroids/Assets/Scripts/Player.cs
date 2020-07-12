using System.Collections;
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

	public delegate void OnPlayerDestroy();

	public OnPlayerDestroy onPlayerDestroyCallback;

	[SerializeField]
	private float mainForce = 1000f;

	[SerializeField]
	private float rotationForce = 200f;

	[SerializeField]
	private AudioClip destroyAudioClip = default;

	private AudioSource playerAudioSource;

	private Rigidbody2D playerRigidbody2D;

	private float maxAxisVelocity = 42f;

	private bool isAlive = true;

	private void Start()
	{
		playerRigidbody2D = GetComponent<Rigidbody2D>();

		playerAudioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		StartCoroutine(DestroyPlayer());
	}

	public IEnumerator DestroyPlayer()
	{
		isAlive = false;
		playerRigidbody2D.velocity = Vector2.zero;
		playerRigidbody2D.freezeRotation = true;

		if (destroyAudioClip != null)
		{
			if (playerAudioSource != null)
			{
				playerAudioSource.PlayOneShot(destroyAudioClip, 1f);
			}
			else
			{
				Debug.Log("Audio source is equal to null, but audio clip exist");
			}

			yield return new WaitForSeconds(destroyAudioClip.length);
		}

		if (onPlayerDestroyCallback != null)
		{
			onPlayerDestroyCallback();
		}

		gameObject.SetActive(false);
	}

	public void AddRelativeForce(ForceDirection forceDirection)
	{
		if (isAlive == true)
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

			playerRigidbody2D.AddRelativeForce(mainForce * Time.deltaTime * forceDirectionVector);

			if (Mathf.Abs(playerRigidbody2D.velocity.x) > maxAxisVelocity)
			{
				float newVelocity = (playerRigidbody2D.velocity.x > 0) ? maxAxisVelocity : -1 * maxAxisVelocity;
				playerRigidbody2D.velocity = new Vector2(newVelocity, playerRigidbody2D.velocity.y);
			}

			if (Mathf.Abs(playerRigidbody2D.velocity.y) > maxAxisVelocity)
			{
				float newVelocity = (playerRigidbody2D.velocity.y > 0) ? maxAxisVelocity : -1 * maxAxisVelocity;
				playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, newVelocity);
			}
		}
	}

	public void AddRotation(RotationDirection rotationDirection)
	{
		if (isAlive == true)
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

			playerRigidbody2D.rotation += rotationForce * Time.deltaTime * rotationDirectionValue;
		}
	}
}
