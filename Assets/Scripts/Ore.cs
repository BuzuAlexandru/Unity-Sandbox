using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ore : MonoBehaviour
{
    public string type;
    public float maxHP;
    public float hitPoints;
    public PolygonCollider2D pc;
    public SpriteRenderer sr;
    public Slider hpBar;
    public float respawn = 60f;
    void Start()
    {
        hitPoints = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = hitPoints;
    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D theCollision){
        if (theCollision.CompareTag("Bullet"))
        {
            hitPoints -= 1f;
            hpBar.value = hitPoints;
            if(hitPoints == 0){
                pc.enabled = false;
                sr.enabled = false;
                hpBar.gameObject.SetActive(false);
                StartCoroutine(RespawnDelay());
            }
        }
    }

    IEnumerator RespawnDelay(){
        yield return new WaitForSeconds(respawn);
        pc.enabled = true;
        sr.enabled = true;
        hpBar.gameObject.SetActive(true);
        hitPoints = maxHP;
        hpBar.value = hitPoints;
    }

}
