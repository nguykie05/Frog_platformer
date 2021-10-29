using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;

    public GameObject enemyDeathFX;
    public Slider eHealth;

    public bool drops;
    public GameObject theDrop;

    private float currentHealth;

    private void Start()
    {
        currentHealth = enemyMaxHealth;
        eHealth.maxValue = currentHealth;
        eHealth.value = currentHealth;
    }
    public void addDamage(float damage)
    {
        currentHealth -= damage;
        eHealth.value = currentHealth;

        if (currentHealth <= 0) makeDead();
    }

    void makeDead()
    {
        Destroy(gameObject);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        if (drops) Instantiate(theDrop, transform.position, transform.rotation);
    }
}
