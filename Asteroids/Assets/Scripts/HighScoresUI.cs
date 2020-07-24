using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoresUI : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> scoresTextMeshPro = default;

    private HighScores highScores;

    private void Start()
    {
        highScores = HighScores.Instance;

        for (int i = 0; i < HighScores.SCORES_LENGTH && i < scoresTextMeshPro.Count; i++)
        {
            scoresTextMeshPro[i].text = highScores.GetScore(i).ToString();
        }

        if (HighScores.SCORES_LENGTH != scoresTextMeshPro.Count)
        {
            Debug.LogWarning("Scores lenght = " + HighScores.SCORES_LENGTH +
                ", but scores TextMeshPro lists count = " + scoresTextMeshPro.Count);
        }
    }
}
