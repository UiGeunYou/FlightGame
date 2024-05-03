using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        //��ƼŬ�� ������� �ƴϸ� ����
        if( _particle.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
