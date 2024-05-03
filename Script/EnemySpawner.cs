using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData; //스테이지 크기 정보
    [SerializeField] private GameObject enemyPrefab;//생성할 적 프리팹
    [SerializeField] private float spawnTime;//생성 주기
    [SerializeField] private GameObject enemyHpSliderPrefab; //적 체력을 나타내는 Slider UI 프리팹
    [SerializeField] private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    [SerializeField] private BgmController bgmController;
    [SerializeField] private GameObject textBossWarning;
    [SerializeField] private GameObject panelBossHp;
    [SerializeField] private GameObject boss;
    [SerializeField] private int maxEnemyCnt = 100;

    private void Awake()
    {
        textBossWarning.SetActive(false);
        panelBossHp.SetActive(false);
        boss.SetActive(false);
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCnt = 0; //적 생성 숫자 카운트

        while (true)
        {
            // x위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //적 생성 위치
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            //적 캐릭터 생성
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            //적 체력을 나타내는 Slider UI 생성
            SpawnEnemyHpSlider(enemyClone);

            currentEnemyCnt++;
            if(currentEnemyCnt == maxEnemyCnt)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            //spawnTime만큼 대기
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHpSlider(GameObject _enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI를 캔버스의 자식으로 설정
        sliderClone.transform.SetParent(canvasTransform);
        //계층 설정으로 바뀐 크기를 다시(1, 1, 1)로 설정
        sliderClone.transform.localScale = Vector3.one;
        //Slider UI가 추적할 대상으로 지정
        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(_enemy.transform);
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHpViewer>().SetUp(_enemy.GetComponent<EnemyHp>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBgm(BGMType.Boss);
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        textBossWarning.SetActive(false);
        panelBossHp.SetActive(true);
        boss.SetActive(true);
        //보스를 지정된 위치로 이동
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
