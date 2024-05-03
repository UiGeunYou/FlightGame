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
        //Slider UI�� �Ѿƴٴ� target ���� 
        targetTransform = _target;
        //RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {   //������Ʈ�� ��ġ�� ���ŵ� �Ŀ� Slider UI�� �Բ� ��ġ�� �����ϱ� ���� LateUpdat���� ȣ���Ѵ�.
        //���� �ı��Ǹ� Slider UI�� ���� 
        if (targetTransform==null)
        {
            Destroy(gameObject);
            return;
        }

        //������Ʈ ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //ȭ�鳻���� ��ǥ + distance��ŭ ������ ��ġ�� Slider UI�� ��ġ�� ��
        rectTransform.position = screenPosition + distance;
    }
}
