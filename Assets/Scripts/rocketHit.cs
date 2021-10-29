using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class rocketHit : MonoBehaviour
{
    public float weaponDamage;

    private projectileController myPC;
    //calls the projectileController and calls it myPC

    public GameObject explosionEffect;
    //reference to the particle explosion

    //Use this for initialization
    private void Awake()
    {
        myPC = GetComponentInParent<projectileController>();
        //assigns myPC to the component in the parent (projectile)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            //says if the gameObject hits an object under the "Shootable" layer...
            myPC.removeForce();
            //...call the removeForce from the parent

            Instantiate(explosionEffect, transform.position, transform.rotation);
            //then replace the object with the explosion effect

            Destroy(gameObject);
            //then destroy the gameObject

            if (other.tag == "Enemy")
            {
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            if (other.tag == "Enemy")
            {
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
            //this is a safeguard script.
        }
    }
}
