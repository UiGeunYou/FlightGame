using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{
   [SerializeField] private PlayerHp playerHp;
    private Slider sliderHp;

    private void Awake()
    {
        sliderHp = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHp.value = playerHp.CurrentHp / playerHp.MaxHp;
    }
}
