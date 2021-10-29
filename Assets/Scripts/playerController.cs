using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //movement variables
    public float maxSpeed;
    public bool facingRight;

    //jump variables
    private bool grounded = false;
    private float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    private Rigidbody2D myRB;
    private Animator myAnim;

    //for shooting
    public Transform gunTip;
    public GameObject bullet;
    public float fireRate = 0.5f;
    private float nextFire = 0f;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    void FixedUpdate()
    {
        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        //player shooting
        if (Input.GetAxisRaw("Fire1") > 0) fireRocket();

        //check if we are grounded, if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        float move = Input.GetAxis("Horizontal");

        myAnim.SetFloat("speed", Mathf.Abs(move));

        //Input only gives a value between 
        //-1 and 0 if going left 
        //and 0 and 1 if going right
        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        if (move > 0 && !facingRight)
        {
            flip();
        } 
        else if (move < 0 && facingRight)
        {
            flip();
        }

        void flip()
        {
            facingRight =! facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }

        void fireRocket()
        {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (facingRight)
                {
                    Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                } else if (!facingRight)
                {
                    Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
                }
            }
        }
    }
}
  