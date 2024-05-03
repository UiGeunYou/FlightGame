using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.down * 35.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void SetUp(Transform _target)
    {
        //Slider UI가 쫓아다닐 target 설정 
        targetTransform = _target;
        //RectTransform 컴포넌트 정보 얻어오기
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {   //오브젝트의 위치가 갱신된 후에 Slider UI도 함께 위치를 설정하기 위해 LateUpdat에서 호출한다.
        //적이 파괴되면 Slider UI도 삭제 
        if (targetTransform==null)
        {
            Destroy(gameObject);
            return;
        }

        //오브젝트 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //화면내에서 좌표 + distance만큼 떨어진 위치를 Slider UI의 위치로 설
        rectTransform.position = screenPosition + distance;
    }
}
