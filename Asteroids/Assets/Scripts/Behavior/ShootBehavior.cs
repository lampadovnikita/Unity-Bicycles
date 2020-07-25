using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
	[SerializeField]
	private Pool bulletPool = default;

	[SerializeField]
	private float shootForce = 1500f;

	[SerializeField]
	private AudioClip shootAudioClip = default;

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, 0.3f);
	}

	public void Shoot()
	{
		GameObject bullet = bulletPool.GetObject();
		bullet.transform.position = gameObject.transform.position;
		bullet.transform.rotation = gameObject.transform.rotation;

		Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
		if (bulletRigidbody2D != null)
		{
			bulletRigidbody2D.rotation = gameObject.transform.rotation.eulerAngles.z;
			bulletRigidbody2D.AddRelativeForce(shootForce * Vector2.up);
		}
		else
		{
			Debug.LogError("Bullet has no Rigidbody2D component!");
			return;
		}


		if (shootAudioClip != null)
		{
			AudioManager.Instance.EffectsAudioSource.PlayOneShot(shootAudioClip);
		}
	}
}
