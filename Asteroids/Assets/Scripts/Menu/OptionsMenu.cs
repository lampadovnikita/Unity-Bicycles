using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private static float generalVolume = 0f;
    private static float musicVolume = 0f;
    private static float effectsVolume = 0f;
    
    [SerializeField]
    private AudioMixer generalAudioMixer = default;

    [SerializeField]
    private Slider generalVolumeSlider = default;

    [SerializeField]
    private Slider musicVolumeSlider = default;

    [SerializeField]
    private Slider effectsVolumeSlider = default;


    public void Start()
    {
        generalVolumeSlider.value = generalVolume;
        musicVolumeSlider.value = musicVolume;
        effectsVolumeSlider.value = effectsVolume;
    }

    public void SetGeneralVolume(float volume)
    {
        generalAudioMixer.SetFloat("GeneralVolume", volume);
        generalVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        generalAudioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        generalAudioMixer.SetFloat("EffectsVolume", volume);
        effectsVolume = volume;
    }

}
