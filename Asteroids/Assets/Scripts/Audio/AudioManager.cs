using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[SerializeField]
	private AudioSource _MusicAudioSource = default;

	[SerializeField]
	private AudioSource _EffectsAudioSource = default;

	[SerializeField]
	private AudioMixer generalAudioMixer = default;

	public AudioSource EffectsAudioSource
	{
		get
		{
			return _EffectsAudioSource;
		}
	}

	public AudioSource MusicAudioSource
	{
		get
		{
			return _MusicAudioSource;
		}
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;		
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		OptionsData data = OptionsSerializer.Load();
		if (data != null)
		{
			SetGeneralVolume(data.generalVolume);
			SetMusicVolume(data.musicVolume);
			SetEffectsVolume(data.effectsVolume);
		}
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
}
