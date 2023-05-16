using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float hitPoints;
    public Rigidbody2D rb;
    void Start()
    {
        
    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D theCollision){
        if (theCollision.CompareTag("Ladder"))
        {
            hitPoints -= 1f;
        }
    }

}
