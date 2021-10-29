using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{

    public float rocketSpeed;

    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();

        if (transform.localRotation.z > 0)
            //applying force to the object
            myRB.AddForce(new Vector2(-1, 0) * rocketSpeed, ForceMode2D.Impulse);
            //the script above will travel in the x for a value of 1. ForceMode2D give an explosive "force"
         else myRB.AddForce(new Vector2(1, 0) * rocketSpeed, ForceMode2D.Impulse);   
    }
        
    public void removeForce()
    {
        myRB.velocity = new Vector2(0, 0);
    }
}
