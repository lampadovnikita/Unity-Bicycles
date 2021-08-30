using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TowerBlocks
{
	public class SettingsMenu : MonoBehaviour
	{
		[SerializeField]
		private Slider generalVolumeSlider = default;

		private void Awake()
		{
			Assert.IsNotNull(generalVolumeSlider);
		}

		private void Start()
		{
			generalVolumeSlider.value = AudioController.Instance.GetGeneralVolume();

			generalVolumeSlider.onValueChanged.AddListener(OnGeneralVolumeSliderChanged);
		}

		private void OnGeneralVolumeSliderChanged(float value)
		{
			AudioController.Instance.SetGeneralVolume(value);
		}
	}

}