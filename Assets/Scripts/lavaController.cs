using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaController : MonoBehaviour
{

    public float lavaSpeedHigh;
    public float lavaSpeedLow;
    public float lavaAngle;
    public float lavaTorqueAngle;

    private Rigidbody2D lavaRB;

    // Start is called before the first frame update
    void Start()
    {
        lavaRB = GetComponent<Rigidbody2D>();
        lavaRB.AddForce(new Vector2(Random.Range(-lavaAngle, lavaAngle), Random.Range(lavaSpeedLow, lavaSpeedHigh)), ForceMode2D.Impulse);
        lavaRB.AddTorque(Random.Range(-lavaTorqueAngle, lavaTorqueAngle));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
