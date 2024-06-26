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

            //boomDelay에 설정된 시간동안 startPosition부터 endPosition까지 이동
            //curve에 설정된 그래프처럼 빠르게 이동하고, 목적지에 다다를수록 천천히 이동
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }
        //이동이 완료된 후 애니메이션 변경
        animator.SetTrigger("onBoom");
        audioSource.clip = boomAudio;
        audioSource.Play();
    }

    public void onBoom()
    {
        //모든 Enemy, Meteorite 태그를 가진 모든 오브젝트 정보를 가져온다
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
