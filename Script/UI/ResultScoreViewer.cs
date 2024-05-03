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
        //Stage���� ������ ������ �ҷ��ͼ� score ������ ����
        int score = PlayerPrefs.GetInt("Score");
        //texteResultScore UI�� ������ ����
        textResultScore.text = "Result Score " + score;
    }
}
