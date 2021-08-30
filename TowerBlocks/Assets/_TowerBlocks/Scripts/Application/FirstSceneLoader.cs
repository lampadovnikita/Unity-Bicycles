using UnityEngine;

namespace TowerBlocks
{ 
	// Preload Scene load DontDestroyOnLoad data for the whole application
	// So this class load main menu after data created
    public class FirstSceneLoader : MonoBehaviour
    {
		public void Update()
		{
#if UNITY_EDITOR == false
			SceneMethods.LoadMainMenu();
#endif
		}
	}
}

