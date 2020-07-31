using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneLoader : MonoBehaviour
{
    private const int MENU_SCENE_INDEX = 1;

    void Start()
    {
        SceneManager.LoadScene(MENU_SCENE_INDEX);
    }
}
