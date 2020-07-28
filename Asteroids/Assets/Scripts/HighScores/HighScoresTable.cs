using TMPro;
using UnityEngine;

public class HighScoresTable : MonoBehaviour
{
	[SerializeField]
	private Transform scoresContainer = default;

	[SerializeField]
	private Transform lineTemplate = default;

	[SerializeField]
	private int linesLimit = 5;

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

		int lineCount = Mathf.Min(linesLimit, HighScores.Instance.ScoresData.scores.Length);

		float gapHeight = (containerHeight - lineCount * lineTemplateHeight) / (lineCount - 1);

		Transform lineTransform;
		RectTransform lineRectTransform;
		float score;
		for (int i = 0; i < lineCount; i++)
		{
			lineTransform = Instantiate(lineTemplate, scoresContainer);
			lineRectTransform = lineTransform.GetComponent<RectTransform>();
			lineRectTransform.anchoredPosition = new Vector2(0, -(lineTemplateHeight + gapHeight) * i);
			lineTransform.gameObject.SetActive(true);

			lineTransform.Find("Line Number Text").GetComponent<TextMeshProUGUI>().text = (i + 1) + ". ";

			score = HighScores.Instance.ScoresData.scores[i];
			lineTransform.Find("Score Text").GetComponent<TextMeshProUGUI>().text = score.ToString();
		}
	}
}
