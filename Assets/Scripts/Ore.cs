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
        Color newCol;
        if(type == "uranium"){
            if (ColorUtility.TryParseHtmlString("#00FF0F", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "aluminum"){
            if (ColorUtility.TryParseHtmlString("#FFC388", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "beryllium"){
            if (ColorUtility.TryParseHtmlString("#FF332F", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "gold"){
            if (ColorUtility.TryParseHtmlString("#CABD00", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "lithium"){
            if (ColorUtility.TryParseHtmlString("#FF33F1", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "plutonium"){
            if (ColorUtility.TryParseHtmlString("#30A130", out newCol))
            {
                sr.color = newCol;
            }
        }
        if(type == "titanium"){
            if (ColorUtility.TryParseHtmlString("#414141", out newCol))
            {
                sr.color = newCol;
            }
        }
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
