using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Ingot : MonoBehaviour
{
    public string type;
    public SpriteRenderer sr;
    public TMP_Text text;

    public void SetType(string str)
    {
        type = str;
        text.text = ToPascalCase(str);
        sr.sprite = Resources.Load<Sprite>(string.Concat("Inventory/", type));
    }

    
    string ToPascalCase(string str)
    {
        return  char.ToUpper(str[0]) + str.Substring(1);
    }

    void OnTriggerEnter2D(Collider2D theCollision){
        if (theCollision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
