using UnityEngine;

namespace TowerBlocks
{ 
    public static class ApplicationMethods
    {
        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();
        }

        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}