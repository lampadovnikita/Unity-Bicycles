using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TowerBlocks
{ 
	public class MainMenu : MonoBehaviour
	{
		[SerializeField]
		private ModalWindow exitGameModalWindow = default;

		[SerializeField]
		private ModalWindow newGameModalWindow = default;

		[SerializeField]
		private Button continueButton = default;

		private void Awake()
		{
			Assert.IsNotNull(exitGameModalWindow);
			Assert.IsNotNull(newGameModalWindow);
		}

		private void Start()
		{
			exitGameModalWindow.OnWindowClosed += OnExitGameModalClosed;
			newGameModalWindow.OnWindowClosed += OnNewGameModalClosed;

			SaveData data = SaveLoader.Load();

			if (data.gameProgressData.levelIndex == 0)
			{
				continueButton.interactable = false;
			}
		}

		public void StartNewGame()
		{
			SaveData data = SaveLoader.Load();

			if (data.gameProgressData.levelIndex > 0)
			{
				newGameModalWindow.Show();
			}
			else
			{
				LoadGame();
			}
		}

		public void LoadGame()
		{
			SceneMethods.LoadGame();
		}

		private void QuitGame()
		{
			ApplicationMethods.QuitGame();
		}

		private void OnExitGameModalClosed(ModalWindow modalWindow, ModalWindowResult result)
		{
			if (result == ModalWindowResult.OK)
			{
				QuitGame();
			}
		}

		private void OnNewGameModalClosed(ModalWindow modalWindow, ModalWindowResult result)
		{
			if (result == ModalWindowResult.OK)
			{
				SaveLoader.Save(new SaveData(new GameProgressData()));

				LoadGame();
			}
		}
	}
}