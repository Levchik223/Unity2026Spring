using System;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
            Debug.Log("Near Player");
    }
}
