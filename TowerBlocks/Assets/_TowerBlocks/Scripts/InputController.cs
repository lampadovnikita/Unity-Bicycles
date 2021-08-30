using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace TowerBlocks
{
	public class InputController : MonoBehaviour
	{
		[SerializeField]
		private BlockSpawner blockSpawner = default;

		private void Awake()
		{
			Assert.IsNotNull(blockSpawner);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) == true)
			{
				if (EventSystem.current.IsPointerOverGameObject() == false)
				{ 
					blockSpawner.AttemptFreeBlock();
				}
			}
		}
	}
}
