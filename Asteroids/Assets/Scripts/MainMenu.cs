using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
