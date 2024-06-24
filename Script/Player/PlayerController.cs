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
    private Movement2D movement2D;
    private Weapon weapon;
    private Animator animator;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager.instance.isDie == true) return;

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
        //�̵� ���� �ʱ�ȭ
        movement2D.MoveTo(Vector3.zero);
        //��� �ִϸ��̼� ���
        animator.SetTrigger("onDie");
        //�浹�ڽ� ����
        Destroy(GetComponent<CircleCollider2D>());
        //��� �� ���� �Ҵ�
        GameManager.instance.isDie = true;
    }

    public void onDieEvent()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
