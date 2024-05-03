using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition}

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;
        int count = 30;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        //원 형태로 발사되는 발사체 생성
        while (true)
        {
            for(int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                //발사체 이동 방향 설정
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
             weightAngle += intervalAngle;

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;

        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //발사체 이동 방향
            Vector3 direction = (targetPosition - clone.transform.position).normalized;

            clone.GetComponent<Movement2D>().MoveTo(direction);

            yield return new WaitForSeconds(attackRate);
        }
    }
}
