using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer generalAudioMixer = default;

    public void SetGeneralVolume(float volume)
    {
        generalAudioMixer.SetFloat("GeneralVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        generalAudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        generalAudioMixer.SetFloat("EffectsVolume", volume);
    }

}
