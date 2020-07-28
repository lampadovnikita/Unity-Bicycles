using TMPro;
using UnityEngine;

public class HighScoresTable : MonoBehaviour
{
	[SerializeField]
	private Transform scoresContainer = default;

	[SerializeField]
	private Transform lineTemplate = default;

	[SerializeField]
	private int lineCount = 5;


	private void Awake()
	{
		lineTemplate.gameObject.SetActive(false);
	}

	private void Start()
	{
		RectTransform containerRectTransform = scoresContainer.GetComponent<RectTransform>();
		RectTransform lineTemplateRectTransform = lineTemplate.GetComponent<RectTransform>();

		float containerHeight = containerRectTransform.rect.height;
		float lineTemplateHeight = lineTemplateRectTransform.rect.height;

		float gapHeight = (containerHeight - lineCount * lineTemplateHeight) / (lineCount - 1);

		int score = 0;
		for (int i = 0; i < lineCount; i++)
		{
			Transform lineTransform = Instantiate(lineTemplate, scoresContainer);
			RectTransform lineRectTransform = lineTransform.GetComponent<RectTransform>();
			lineRectTransform.anchoredPosition = new Vector2(0, -(lineTemplateHeight + gapHeight) * i);
			lineTransform.gameObject.SetActive(true);

			lineTransform.Find("Line Number Text").GetComponent<TextMeshProUGUI>().text = (i + 1) + ". ";

			if (i < HighScores.Instance.ScoresData.scores.Length)
			{
				score = HighScores.Instance.ScoresData.scores[i];
			}
			else
			{
				score = 0;
			}
			lineTransform.Find("Score Text").GetComponent<TextMeshProUGUI>().text = score.ToString();
		}
	}
}
