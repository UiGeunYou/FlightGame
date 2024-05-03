using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AudioClip boomAudio;
    [SerializeField] private int damage = 100;
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;
        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current/boomDelay;

            //boomDelay�� ������ �ð����� startPosition���� endPosition���� �̵�
            //curve�� ������ �׷���ó�� ������ �̵��ϰ�, �������� �ٴٸ����� õõ�� �̵�
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }
        //�̵��� �Ϸ�� �� �ִϸ��̼� ����
        animator.SetTrigger("onBoom");
        audioSource.clip = boomAudio;
        audioSource.Play();
    }

    public void onBoom()
    {
        //��� Enemy, Meteorite �±׸� ���� ��� ������Ʈ ������ �����´�
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        for(int i = 0; i < enemys.Length; ++i)
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }

        for (int i = 0; i < meteorites.Length; ++i)
        {
            meteorites[i].GetComponent<Meteorite>().OnDie();
        }

        for (int i = 0; i < projectiles.Length; ++i)
        {
            projectiles[i].GetComponent<EnemyProjectile>().OnDie();
        }

        if(boss != null)
        {
            boss.GetComponent<BossHp>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
