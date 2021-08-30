using UnityEngine.SceneManagement;

namespace TowerBlocks
{ 
    public class SceneMethods
    {
        private const string GAME_SCENE_NAME = "Game";
        private const string MAIN_MENU_SCENE_NAME = "Main Menu";

	    public static void LoadGame()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        public static void LoadMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }
    }
}

