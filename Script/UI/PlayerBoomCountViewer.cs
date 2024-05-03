using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private TextMeshProUGUI textBoomCnt;

    private void Awake()
    {
        textBoomCnt = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        textBoomCnt.text = "x "+ weapon.BoomCnt;
    }
}
