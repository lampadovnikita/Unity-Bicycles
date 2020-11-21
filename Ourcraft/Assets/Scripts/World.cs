using UnityEngine;

public class World : MonoBehaviour
{
	[SerializeField]
	private GameObject cubePrefab = default;

	[SerializeField]
	private int width = default;

	[SerializeField]
	private int lenght = default;

	[SerializeField]
	private int height = default;

	private GameObject[, ,] grid;

	private void Awake()
	{
		grid = new GameObject[width, lenght, height];

		GenerateWorld();
	}

	void Start()
	{

	}

	void Update()
	{

	}

	private void GenerateWorld()
	{
		Vector3 position = new Vector3();

		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < lenght; z++)
			{
				for (int y = 0; y < height; y++)
				{ 
					position.x = x;
					position.y = -y;
					position.z = z;

					grid[x, z, y] = Instantiate(cubePrefab, position, Quaternion.identity, transform);
					grid[x, z, y].transform.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 0f, 0f, 0f, 0.6f, 0.8f);
				}
			}
		}
	}
}
