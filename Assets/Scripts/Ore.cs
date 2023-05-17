using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ore : MonoBehaviour
{
    public string type;
    public float maxHP;
    public float hitPoints;
    public PolygonCollider2D pc;
    public SpriteRenderer sr;
    public TMP_Text display;
    public Slider hpBar;
    public Ingot ingotPrefab;
    public float respawn = 30f;
    void Start()
    {
        hitPoints = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = hitPoints;
        display.text = string.Concat(ToPascalCase(type), " Ore");
    }

    string ToPascalCase(string str)
    {
        return  char.ToUpper(str[0]) + str.Substring(1);
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
                display.enabled = false;
                Ingot ingot = Instantiate(ingotPrefab,transform.position, transform.rotation);
                ingot.SetType(type);
                StartCoroutine(RespawnDelay());
            }
        }
    }

    IEnumerator RespawnDelay(){
        yield return new WaitForSeconds(respawn);
        pc.enabled = true;
        sr.enabled = true;
        display.enabled = true;
        hpBar.gameObject.SetActive(true);
        hitPoints = maxHP;
        hpBar.value = hitPoints;
    }

}
