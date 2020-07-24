using UnityEngine;

public class MainMusicLoader : MonoBehaviour
{
    [SerializeField]
    private AudioClip MainMusicTheme = default;

    private void Start()
    {
        AudioManager instance = AudioManager.Instance;
        instance.MusicAudioSource.clip = MainMusicTheme;
        instance.MusicAudioSource.loop = true;
        instance.MusicAudioSource.Play();
    }
}
