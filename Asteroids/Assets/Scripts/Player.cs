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

	public delegate void PlayerDestroy();
	public event PlayerDestroy OnPlayerDestroy;

	[SerializeField]
	private SpriteRenderer playerSpriteRenderer = default;

	[SerializeField]
	private float mainForce = 1000f;

	[SerializeField]
	private float rotationForce = 200f;

	[SerializeField]
	private AudioClip destroyAudioClip = default;

	[SerializeField]
	private float maxAxisVelocity = 42f;

	[SerializeField]
	private float respawnInvulnerableTime = 3f; // In seconds

	[SerializeField]
	private int InvulnerableBlinkNumber = 3;

	private Rigidbody2D playerRigidbody2D;

	private bool IsInvulnerable { get; set; }

	private void Awake()
	{
		playerRigidbody2D = GetComponent<Rigidbody2D>();

		IsInvulnerable = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsInvulnerable == false)
		{ 
			DestroyPlayer();
		}
	}

	public void Respawn()
	{
		transform.position = new Vector3(0f, 0f, transform.position.z);
		transform.rotation = Quaternion.identity;
		gameObject.SetActive(true);
		StartCoroutine(TemporaryInvulnerability());
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

		playerRigidbody2D.rotation += rotationForce * Time.deltaTime * rotationDirectionValue;
	}

	private void DestroyPlayer()
	{
		if (destroyAudioClip != null)
		{
			AudioManager.Instance.EffectsAudioSource.PlayOneShot(destroyAudioClip);
		}

		OnPlayerDestroy?.Invoke();

		gameObject.SetActive(false);
	}

	private IEnumerator TemporaryInvulnerability()
	{
		IsInvulnerable = true;

		Color spriteColor = playerSpriteRenderer.color;
		float originalAlpha = spriteColor.a;

		float blinkHalfPeriod = respawnInvulnerableTime / (InvulnerableBlinkNumber * 2);
		for (int i = 0; i < InvulnerableBlinkNumber; i++)
		{
			yield return new WaitForSeconds(blinkHalfPeriod);

			spriteColor.a = 0f;
			playerSpriteRenderer.color = spriteColor;

			yield return new WaitForSeconds(blinkHalfPeriod);

			spriteColor.a = originalAlpha;
			playerSpriteRenderer.color = spriteColor;
		}

		IsInvulnerable = false;
	}
}
