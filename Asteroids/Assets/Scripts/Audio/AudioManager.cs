using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[SerializeField]
	private AudioSource _MusicAudioSource = default;

	[SerializeField]
	private AudioSource _EffectsAudioSource = default;

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
}
