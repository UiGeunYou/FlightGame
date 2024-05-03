using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private StageData stageData;
    [SerializeField] KeyCode keyCodeAttack = KeyCode.Space;
    [SerializeField] KeyCode keyCodeBoom = KeyCode.Z;
    private bool isDie = false;
    private Movement2D movement2D;
    private Weapon weapon;
    private Animator animator;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == true) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3 (x, y, 0));

        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if(Input.GetKeyUp(keyCodeAttack)) 
        {
            weapon.StopFiring();
        }
        else if (Input.GetKeyUp(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x), 
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void OnDie()
    {
        //이동 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        //사망 애니메이션 재생
        animator.SetTrigger("onDie");
        //충돌박스 삭제
        Destroy(GetComponent<CircleCollider2D>());
        //사망 시 조작 불능
        isDie = true;
    }

    public void onDieEvent()
    {
        //디바이스에 획득한 점수 저장
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(nextSceneName);
    }
}
