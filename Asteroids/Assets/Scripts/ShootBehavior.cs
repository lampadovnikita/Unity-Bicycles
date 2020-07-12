using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
	[SerializeField]
	private BulletBehavior bulletPrefab = default;

	[SerializeField]
	private AudioClip shootAudioClip = default;

	private AudioSource audioSource = default;

	public void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Shoot()
	{
		Instantiate(bulletPrefab, transform.position, transform.rotation);

		if (shootAudioClip != null)
		{
			if (audioSource != null)
			{
				audioSource.PlayOneShot(shootAudioClip, 0.5f);
			}
			else
			{
				Debug.Log("Audio source is equal to null, but audio clip exist");
			}
		}
	}
}
