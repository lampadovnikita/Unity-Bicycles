using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
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
        OptionsData data = OptionsSerializer.Load();
        if (data != null)
        { 
            generalVolumeSlider.value = data.generalVolume;
            musicVolumeSlider.value = data.musicVolume;
            effectsVolumeSlider.value = data.effectsVolume;        
        }
    }

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

    public void SaveOptions()
    {
        OptionsData data = new OptionsData
        {
            generalVolume = generalVolumeSlider.value,
            musicVolume = musicVolumeSlider.value,
            effectsVolume = effectsVolumeSlider.value
        };

        OptionsSerializer.Save(data);
    }
}
