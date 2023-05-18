using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crafting : MonoBehaviour
{
    public GameObject craftingUI;  

    public UnityEngine.UI.Button button; 
    public UnityEngine.UI.Button button1; 
    public UnityEngine.UI.Button button2; 
    public UnityEngine.UI.Button button3; 
    public UnityEngine.UI.Button button4; 
    public UnityEngine.UI.Button button5; 
    public Player player;
    

    private void Start()
    {
        UpdateCraftingUI();
        button.onClick.AddListener(delegate {
            Craft(1);
        });
        button1.onClick.AddListener(delegate {
            Craft(2);
        });
        button2.onClick.AddListener(delegate {
            Craft(3);
        });
        button3.onClick.AddListener(delegate {
            Craft(4);
        });
        button4.onClick.AddListener(delegate {
            Craft(5);
        });
        button5.onClick.AddListener(delegate {
            Craft(6);
        });
    }

    void UpdateCraftingUI(){
        
    }

    void Craft(int recipe){
        if(recipe == 1){
            player.updateButton1();
        } else if(recipe == 2){
            player.updateButton2();
        } else if(recipe == 3){
            player.updateButton3();
        } else if(recipe == 4){
            player.updateButton4();
        } else if(recipe == 5){
            player.updateButton5();
        } else if(recipe == 6){
            player.updateButton6();
        }
    }

}
