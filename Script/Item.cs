using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { PowerUp = 0, Boom, Hp}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    private Movement2D movement2d;

    private void Awake()
    {
        movement2d = GetComponent<Movement2D>();

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);
        
        movement2d.MoveTo(new Vector3(x,y,0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Use(GameObject _player)
    {
        switch (itemType)
        {
            case ItemType.PowerUp:
                _player.GetComponent<Weapon>().AttackLevel++;
                break;
            case ItemType.Boom:
                _player.GetComponent<Weapon>().BoomCnt++;
                break;
            case ItemType.Hp:
                _player.GetComponent<PlayerHp>().CurrentHp += 2;
                break;
        }
    }
}
