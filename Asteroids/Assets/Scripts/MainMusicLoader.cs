using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicLoader : MonoBehaviour
{
    [SerializeField]
    private AudioClip MainMusicTheme = default;

    private void Start()
    {
        AudioManager instance = AudioManager.Instance;
        instance.GlobalAudioSource.clip = MainMusicTheme;
        instance.GlobalAudioSource.loop = true;
        instance.GlobalAudioSource.Play();
    }
}
