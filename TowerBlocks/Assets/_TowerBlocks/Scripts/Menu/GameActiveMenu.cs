using UnityEngine;

namespace TowerBlocks
{ 
	public class GameActiveMenu : MonoBehaviour
	{
		public void PauseGame()
		{
			ApplicationMethods.PauseGame();
		}
	}
}