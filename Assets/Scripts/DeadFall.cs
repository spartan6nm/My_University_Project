using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFall : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.PlayerDied();
        }
    }
}
