using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace TowerBlocks
{ 
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance { get; private set; }

        [SerializeField]
        private AudioSource sfxAudioSource = default;

        [SerializeField]
        private AudioMixer generalAudioMixer = default;

        public AudioSource SFXAudioSource => sfxAudioSource;

		private void Awake()
		{
            if (Instance == null)
            {
                Assert.IsNotNull(generalAudioMixer);
                Assert.IsNotNull(sfxAudioSource);

                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                
                return;
            }
		}

		private void Start()
		{
            SetGeneralVolume(0.8f);
        }

        public void SetGeneralVolume(float volume)
        {
            generalAudioMixer.SetFloat("General Volume", ToDecibel(volume));
        }

        public float GetGeneralVolume()
        {
            float dbVolume;
            bool res = generalAudioMixer.GetFloat("General Volume", out dbVolume);

            Assert.IsTrue(res);

            return FromDecibel(dbVolume);
        }

        private static float ToDecibel(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1f);

            return 20f * Mathf.Log10(volume);
        }

        private static float FromDecibel(float volume)
        {
            return Mathf.Pow(10f, volume / 20f);
        }
    }
}

