using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
