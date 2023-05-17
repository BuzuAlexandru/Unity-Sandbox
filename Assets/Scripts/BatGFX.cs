using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatGFX : MonoBehaviour
{
    public AIPath aiPath;
    public int damage;
    public Player playerHealth;
    public int maxHealth = 100;
    public int health;

    void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void OnTriggerEnter2D(Collider2D theCollision){
        if (theCollision.CompareTag("Bullet"))
        {
            health -= 50;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
