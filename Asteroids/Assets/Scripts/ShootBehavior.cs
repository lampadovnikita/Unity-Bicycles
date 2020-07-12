using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
	[SerializeField]
	private BulletBehavior bulletPrefab = default;

	[SerializeField]
	private AudioClip shootAudioClip = default;

	public void Shoot()
	{
		Instantiate(bulletPrefab, transform.position, transform.rotation);

		if (shootAudioClip != null)
		{
			AudioManager.Instance.GlobalAudioSource.PlayOneShot(shootAudioClip);
		}
	}
}
