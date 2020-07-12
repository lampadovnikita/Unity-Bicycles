using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInputController : MonoBehaviour
{
	private Player player = default;

	private void Start()
	{
		player = GetComponent<Player>();
	}

	void Update()
	{
		if ((player.isActiveAndEnabled == true) && (Game.CurrentGameState == Game.GameState.Active))
		{
			if (Input.GetAxisRaw("Vertical") > 0)
			{
				player.AddRelativeForce(Player.ForceDirection.Up);
			}
			if (Input.GetAxisRaw("Horizontal") > 0)
			{
				player.AddRotation(Player.RotationDirection.Right);
			}
			if (Input.GetAxisRaw("Vertical") < 0)
			{
				player.AddRelativeForce(Player.ForceDirection.Down);
			}
			if (Input.GetAxisRaw("Horizontal") < 0)
			{
				player.AddRotation(Player.RotationDirection.Left);
			}
			if (Input.GetButtonDown("Fire1"))
			{
				player.GetComponent<ShootBehavior>().Shoot();
			}
		}
	}
}
