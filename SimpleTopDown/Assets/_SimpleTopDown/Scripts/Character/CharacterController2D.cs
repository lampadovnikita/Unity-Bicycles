using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	[RequireComponent(typeof(Character))]
	public class CharacterController2D : MonoBehaviour
	{
		[SerializeField]
		private KeyCode upKey = KeyCode.W;

		[SerializeField]
		private KeyCode leftKey = KeyCode.A;

		[SerializeField]
		private KeyCode downKey = KeyCode.S;

		[SerializeField]
		private KeyCode rightKey = KeyCode.D;

		[SerializeField]
		private KeyCode shootKey = KeyCode.Mouse0;

		private Vector2 moveDirection;

		private Character character;

		private void Awake()
		{
			character = GetComponent<Character>();
			Assert.IsNotNull(character);

			moveDirection = new Vector2();
		}

		private void FixedUpdate()
		{
			UpdateMoveDirection();

			UpdateLookAtDirection();

			if (Input.GetKey(shootKey))
			{
				character.Shoot();
			}
		}

		private void UpdateMoveDirection()
		{
			float moveX = 0f;
			float moveY = 0f;

			if (Input.GetKey(upKey))
			{
				moveY += 1f;
			}
			if (Input.GetKey(leftKey))
			{
				moveX += -1f;
			}
			if (Input.GetKey(downKey))
			{
				moveY += -1f;
			}
			if (Input.GetKey(rightKey))
			{
				moveX += 1f;
			}

			moveDirection.x = moveX;
			moveDirection.y = moveY;
			moveDirection = moveDirection.normalized;
			
			character.SetMoveDirection(moveDirection);
		}

		private void UpdateLookAtDirection()
		{
			character.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
}
