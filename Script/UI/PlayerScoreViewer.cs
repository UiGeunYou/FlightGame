using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textScore.text = "Score : " + GameManager.instance.Score;
    }
}
