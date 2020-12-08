using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	public class ShootSource : MonoBehaviour
	{
		[SerializeField]
		private ObjectPool bulletPool = default;

		[SerializeField]
		private float bulletSpeed = 13f;

		[SerializeField]
		private float shootCooldownTime = 0.5f; // sec

		private float cantShootUntilTime;

		private void Awake()
		{
			Assert.IsNotNull(bulletPool);

			cantShootUntilTime = 0f;
		}

		public void Shoot()
		{
			if (CanShoot() == true)
			{
				GameObject shooted = bulletPool.Get();
				shooted.SetActive(true);
				shooted.transform.position = transform.position;
				shooted.transform.rotation = transform.rotation;

				Rigidbody2D rbody = shooted.GetComponent<Rigidbody2D>();
				Assert.IsNotNull(rbody);

				rbody.velocity = transform.up.normalized * bulletSpeed;

				cantShootUntilTime = Time.time + shootCooldownTime;
			}
		}

		private bool CanShoot()
		{
			if (Time.time > cantShootUntilTime)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
