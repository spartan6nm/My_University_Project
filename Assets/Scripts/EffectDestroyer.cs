using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    [SerializeField] private float delay;


    private void Awake()
    {
        Invoke("Die", delay);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
