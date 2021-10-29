﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementController : MonoBehaviour
{
    public float enemySpeed;

    private Animator enemyAnimator;

    //facing

    public GameObject enemyGraphic;

    private bool canFlip = true;
    private bool facingRight = false;
    private float flipTime = 5f;
    private float nextFlipChance = 0f;

    //attacking

    public float chargeTime;

    private float startChargeTime;
    private bool charging;
    private Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5) flipFacing();
            nextFlipChance = Time.time + flipTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            charging = true;
            startChargeTime = Time.time + chargeTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(startChargeTime < Time.time)
            {
                if (!facingRight) enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
                else enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
                enemyAnimator.SetBool("isCharging", charging);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new UnityEngine.Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", charging);
        }
    }

    void flipFacing()
    {
        if (!canFlip) return;
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }

}
