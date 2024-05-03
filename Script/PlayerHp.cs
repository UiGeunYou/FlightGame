using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private float maxHp = 10;
    private float currentHp;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MaxHp => maxHp;
    public float CurrentHp
    {
        set => currentHp = Mathf.Clamp(value, 0, maxHp);
        get => currentHp;
    }

    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHp <= 0)
        {
            playerController.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
