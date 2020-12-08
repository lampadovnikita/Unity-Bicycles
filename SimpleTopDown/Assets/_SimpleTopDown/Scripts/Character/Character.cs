using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Character : MonoBehaviour, IHittable
	{
		public delegate void CharacterHit(Character caller);
		public event CharacterHit OnCharacterHit;

		public delegate void ScoreChanged(Character caller);
		public event ScoreChanged OnScoreChanged;

		[SerializeField]
		private ShootSource shootSrc = default;

		[SerializeField]
		private float speed = 3f;

		public int Score
		{
			get => score;
			set
			{
				score = value;
				OnScoreChanged?.Invoke(this);
			}
		}

		private int score;

		private Rigidbody2D rbody;

		private Vector2 moveDirection;

		private void Awake()
		{
			Assert.IsNotNull(shootSrc);

			rbody = GetComponent<Rigidbody2D>();
			Assert.IsNotNull(rbody);

			moveDirection = new Vector2();

			score = 0;
		}

		private void FixedUpdate()
		{
			rbody.velocity = moveDirection * speed;
		}

		public void SetMoveDirection(Vector2 direction)
		{
			moveDirection = direction;
		}

		public void LookAt(Vector3 position)
		{
			Vector2 direction = position - transform.position;
			
			const float SPRITE_ROTATION = 90f;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - SPRITE_ROTATION;

			transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
		}

		public void Shoot()
		{
			shootSrc.Shoot();
		}

		public HitResultType Hit(GameObject hitter)
		{
			OnCharacterHit?.Invoke(this);

			return HitResultType.DESTROY;
		}
	}
}

