using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float fullHealth;
    public GameObject deathFX;
    public restartGame theGameManager;
    public Image damageScreen;
    public GameObject hurtMe;
    public Image gameOverScreen;
    public Image winGameScreen;

    private float currentHealth;
    private playerController controlMovement;
    private bool damaged = false;
    private Color damagedColor = new Color(255f, 0f, 0f, 0.5f);
    private Color deadColor = new Color(0f, 0f, 0f, 0.5f);
    private float smoothColor = 5f;

    //HUD Variables
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        controlMovement = GetComponent<playerController>();

        //HUD Initialization
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageScreen.color = damagedColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage (float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        damaged = true;

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void addHealth (float health)
    {
        currentHealth += health;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
            healthSlider.value = currentHealth;
    }

    //creating a function for when we die, either by damage or immediate or instant death
    public void makeDead()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        theGameManager.restartTheGame();
        damageScreen.color = deadColor;

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
    }

    public void winGame()
    {
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
    }
}
