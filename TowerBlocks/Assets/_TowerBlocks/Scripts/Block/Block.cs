using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Block : MonoBehaviour, IPoolable
	{
		public delegate void StateChanged(Block sender);
		public event StateChanged OnStateChanged;

		public event Hide OnHide;

		[SerializeField]
		private float appearTime = 0.2f;

		[SerializeField]
		private SpriteRenderer spriteRenderer = default;

		private BlockState state;
		public BlockState State
		{
			get
			{
				return state;
			}
			private set
			{
				if (state != value)
				{
					state = value;

					OnStateChanged?.Invoke(this);
				}
			}
		}

		public float MaxWorldY => 
			spriteRenderer.transform.position.y + spriteRenderer.bounds.extents.y;

		private bool IsFalling =>
			Vector3.Angle(Vector3.up, transform.up) > 90f;

		public bool IsFreezed
		{
			get
			{
				return rbody.bodyType == RigidbodyType2D.Static;				
			}
			set
			{
				if (value == true)
				{
					rbody.bodyType = RigidbodyType2D.Static;
				}
				else
				{
					rbody.bodyType = RigidbodyType2D.Dynamic;
				}
			}
		}

		public float Width =>
			spriteRenderer.size.x * spriteRenderer.transform.localScale.x;


		private Rigidbody2D rbody;

		private void Awake()
		{
			rbody = GetComponent<Rigidbody2D>();
			Assert.IsNotNull(rbody);

			Assert.IsNotNull(spriteRenderer);
		}

		private void Start()
		{
			Reveal();
		}

		private void Update()
		{
			if (IsFalling == true)
			{
				State = BlockState.FALL;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			GameObject anotherObj = collision.collider.attachedRigidbody.gameObject;

			Block anotherBlock = anotherObj.GetComponent<Block>();
			Foundation foundation = anotherObj.GetComponent<Foundation>();
			
			if ((anotherBlock != null) || (foundation != null))
			{
				if (State == BlockState.FREE)
				{
					State = BlockState.TOWER_MOUNTED;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			FallTrigger fallDetector = collision.gameObject.GetComponent<FallTrigger>();
			if (fallDetector != null)
			{
				if (State == BlockState.FREE)
				{ 
					State = BlockState.FALL;
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(new Vector3(transform.position.x, MaxWorldY, transform.position.z), 0.1f);
		}

		public void Reveal()
		{
			rbody.simulated = true;

			StartCoroutine(nameof(SmoothAppear));
		}

		public void Hide()
		{
			OnHide?.Invoke(gameObject);

			Delegate[] subscribers = OnStateChanged?.GetInvocationList();

			if (subscribers != null)
			{ 
				foreach (Delegate subscriber in subscribers)
				{
					OnStateChanged -= subscriber as StateChanged;
				}
			}
		}

		public bool AtemptFree()
		{
			if (State == BlockState.READY)
			{
				transform.parent = null;
				transform.rotation = Quaternion.identity;

				IsFreezed = false;

				State = BlockState.FREE;

				return true;
			}
			else
			{
				return false;
			}
		}

		private IEnumerator SmoothAppear()
		{
			State = BlockState.APPEARING;

			for (float t = 0; t < appearTime; t += Time.deltaTime)
			{
				float scale = Mathf.InverseLerp(0f, appearTime, t);

				transform.localScale = new Vector3(scale, scale, 0f);

				yield return null;
			}

			transform.localScale = new Vector3(1f, 1f, 0f);

			State = BlockState.READY;
		}
	}
}