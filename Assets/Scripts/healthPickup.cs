using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public float healthAmount;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerHealth theHealth = other.gameObject.GetComponent<playerHealth>();
            theHealth.addHealth(healthAmount);
            Destroy(gameObject);
        }
    }
}
