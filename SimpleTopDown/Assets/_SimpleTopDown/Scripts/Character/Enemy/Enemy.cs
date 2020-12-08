using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{ 
	[RequireComponent(typeof(Character))]
	public class Enemy : MonoBehaviour
	{
		[SerializeField]
		private Character playerCharacter = default;

		// Ignore these layers when deciding to shoot
		[SerializeField]
		private LayerMask ignoredLayers = default;

		private Character controlledCharacter;

		private bool canShootPlayer;

		RaycastHit2D lastHit;

		private void Awake()
		{
			Assert.IsNotNull(playerCharacter);
			
			controlledCharacter = GetComponent<Character>();
			Assert.IsNotNull(controlledCharacter);

			canShootPlayer = false;
		}

		private void FixedUpdate()
		{
			controlledCharacter.LookAt(playerCharacter.transform.position);

			Vector2 direction = (playerCharacter.transform.position - transform.position).normalized;
			
			lastHit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, ~ignoredLayers);

			if (lastHit.collider != null)
			{
				GameObject hitted = lastHit.collider.gameObject;

				if (hitted.GetComponentInParent<Player>() != null)
				{
					controlledCharacter.Shoot();
					canShootPlayer = true;
				}
				else
				{
					canShootPlayer = false;
				}
			}
		}

		private void OnDrawGizmos()
		{
			// Draw line from controlled character position to player position
			if (lastHit.collider != null)
			{
				if (canShootPlayer == true)
				{
					Gizmos.color = Color.green;
				}
				else
				{
					Gizmos.color = Color.red;
				}

				Gizmos.DrawLine(transform.position, playerCharacter.transform.position);
			}
		}
	}
}

