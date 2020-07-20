using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour
{
	[SerializeField]
	private float maxLifeTime = 2f;

	private float lifeTimeStorage;

	private void OnEnable()
	{
		lifeTimeStorage = 0f;
	}

	private void Update()
	{
		lifeTimeStorage += Time.deltaTime;

		if (lifeTimeStorage > maxLifeTime)
		{
			Hide();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Hide();
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
