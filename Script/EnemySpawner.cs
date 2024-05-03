using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData; //�������� ũ�� ����
    [SerializeField] private GameObject enemyPrefab;//������ �� ������
    [SerializeField] private float spawnTime;//���� �ֱ�
    [SerializeField] private GameObject enemyHpSliderPrefab; //�� ü���� ��Ÿ���� Slider UI ������
    [SerializeField] private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
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
        int currentEnemyCnt = 0; //�� ���� ���� ī��Ʈ

        while (true)
        {
            // x��ġ�� ���������� ũ�� ���� ������ ������ ���� ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //�� ���� ��ġ
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            //�� ĳ���� ����
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            //�� ü���� ��Ÿ���� Slider UI ����
            SpawnEnemyHpSlider(enemyClone);

            currentEnemyCnt++;
            if(currentEnemyCnt == maxEnemyCnt)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            //spawnTime��ŭ ���
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHpSlider(GameObject _enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        //Slider UI�� ĵ������ �ڽ����� ����
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ�(1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;
        //Slider UI�� ������ ������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(_enemy.transform);
        //Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
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
        //������ ������ ��ġ�� �̵�
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
