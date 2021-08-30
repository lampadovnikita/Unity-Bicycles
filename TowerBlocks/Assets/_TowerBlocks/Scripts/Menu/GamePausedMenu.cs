using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{ 
    public class GamePausedMenu : MonoBehaviour
    {
		[SerializeField]
		private ModalWindow toMainMenuModalWindow = default;

		private void Awake()
		{
			Assert.IsNotNull(toMainMenuModalWindow);
		}

		private void Start()
		{
			toMainMenuModalWindow.OnWindowClosed += OnToMainMenuModalClosed;
		}

		public void ResumeGame()
        {
            ApplicationMethods.ResumeGame();
        }

		private void LoadMainMenu()
		{
			SceneMethods.LoadMainMenu();
		}

		private void OnToMainMenuModalClosed(ModalWindow modalWindow, ModalWindowResult result)
		{
			if (result == ModalWindowResult.OK)
			{
				LoadMainMenu();
			}
		}
	}
}