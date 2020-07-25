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

	[SerializeField]
	private float templateHeight = 60f;

	private void Awake()
	{
		lineTemplate.gameObject.SetActive(false);

		for (int i = 0; i < lineCount; i++)
		{
			Transform lineTransform = Instantiate(lineTemplate, scoresContainer);
			RectTransform lineRectTransform = lineTransform.GetComponent<RectTransform>();
			lineRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
			lineTransform.gameObject.SetActive(true);

			lineTransform.Find("Line Number Text").GetComponent<TextMeshProUGUI>().text = (i + 1) + ". ";
			lineTransform.Find("Score Text").GetComponent<TextMeshProUGUI>().text = (10000 * i).ToString();
		}
	}
}
