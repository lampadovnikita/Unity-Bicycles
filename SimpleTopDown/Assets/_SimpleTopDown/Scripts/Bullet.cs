using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Bullet : MonoBehaviour, IPoolable
	{
		// LayerMask identifying the surface of the game area
		[SerializeField]
		private LayerMask floorLayerMask = default;

		[SerializeField]
		private float outOfAreaHideDelay = 0.2f; // sec

		public event Hide OnHide;

		private Vector2 velocityBeforeCollision;

		private Rigidbody2D rbody;

		private bool isHided;

		private bool isWaitForHide;

		private void Awake()
		{
			rbody = GetComponent<Rigidbody2D>();
			Assert.IsNotNull(rbody);

			isHided = false;
			isWaitForHide = false;
		}

		private void FixedUpdate()
		{
			// This is necessary to restore the speed value after a collision
			velocityBeforeCollision = rbody.velocity;

			if (isInFrontOfArea() == false && isWaitForHide == false)
			{
				// Prevent multiple hiding, when a bullet flies out of the game area
				isWaitForHide = true;

				Invoke(nameof(HideBullet), outOfAreaHideDelay);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			IHittable hittable = collision.collider.attachedRigidbody.gameObject.GetComponent<IHittable>();
			if (hittable != null)
			{
				HitResultType resultType = hittable.Hit(gameObject);

				if (resultType == HitResultType.DESTROY)
				{
					HideBullet();
				}
				else if (resultType == HitResultType.RICOCHET)
				{
					Ricochet(collision);
				}
			}
		}

		public void Reveal()
		{
			isWaitForHide = false;
			isHided = false;
		}

		private void Ricochet(Collision2D collision)
		{
			Vector2 reflection = Vector2.Reflect(rbody.transform.up, collision.contacts[0].normal);

			float angle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg - 90f;
			transform.eulerAngles = new Vector3(0f, 0f, angle);
			
			rbody.velocity = transform.up * velocityBeforeCollision.magnitude;
		}

		// If a bullet is in front of a surface with a corresponding LayerMask,
		// we assume that it does not leave the game area
		private bool isInFrontOfArea()
		{
			RaycastHit hit;
			Physics.Raycast(transform.position, transform.forward, out hit);

			Collider collider = hit.collider;
			if (collider != null)
			{
				if (floorLayerMask.Contains(collider.gameObject.layer))
				{
					return true;
				}
			}

			return false;
		}

		private void HideBullet()
		{
			// If a bullet waits to hide but collides with a character,
			// we need to prevent double hiding
			if (isHided == false)
			{
				isHided = true;
				OnHide?.Invoke(gameObject);
			}
		}
	}
}
