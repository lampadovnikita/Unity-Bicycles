using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider generalVolumeSlider = default;

    [SerializeField]
    private Slider musicVolumeSlider = default;

    [SerializeField]
    private Slider effectsVolumeSlider = default;

    private AudioManager audioManagerInstance;

    private void Awake()
    {
        audioManagerInstance = AudioManager.Instance;
    }

    private void Start()
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
        audioManagerInstance.SetGeneralVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioManagerInstance.SetMusicVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioManagerInstance.SetEffectsVolume(volume);
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
