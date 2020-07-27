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

        SetGeneralVolume(generalVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetEffectsVolume(effectsVolumeSlider.value);
    }

    public void SetGeneralVolume(float volume)
    {
        generalAudioMixer.SetFloat("GeneralVolume", ToDecibel(volume));
    }

    public void SetMusicVolume(float volume)
    {
        generalAudioMixer.SetFloat("MusicVolume", ToDecibel(volume));
    }

    public void SetEffectsVolume(float volume)
    {
        generalAudioMixer.SetFloat("EffectsVolume", ToDecibel(volume));
    }

    private float ToDecibel(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        
        return 20 * Mathf.Log10(volume);
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
