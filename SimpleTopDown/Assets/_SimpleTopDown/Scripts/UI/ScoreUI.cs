using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField]
		private Character character = default;

		[SerializeField]
		private TextMeshProUGUI scoreTMPro = default;

		private void Awake()
		{
			Assert.IsNotNull(character);
			Assert.IsNotNull(scoreTMPro);
		}

		private void Start()
		{
			character.OnScoreChanged += UpdateScore;

			UpdateScore(character);
		}

		private void UpdateScore(Character caller)
		{
			scoreTMPro.text = caller.Score.ToString();
		}
	}
}