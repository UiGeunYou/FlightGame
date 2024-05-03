using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;

    private void Awake()
    {
        textResultScore = GetComponent<TextMeshProUGUI>();
        //Stage에서 저장한 점수를 불러와서 score 변수에 저장
        int score = PlayerPrefs.GetInt("Score");
        //texteResultScore UI에 점수를 저장
        textResultScore.text = "Result Score " + score;
    }
}
