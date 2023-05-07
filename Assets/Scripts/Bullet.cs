using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(){
        StartCoroutine(DespawnDelay());
    }

    IEnumerator DespawnDelay(){
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

}
