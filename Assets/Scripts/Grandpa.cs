using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Grandpa : MonoBehaviour
{
    public Player player;
    public int dialogueCounter = 0;
    public UnityEngine.UI.Button next;
    public TMP_Text dialogue;
    string[] sentences = {
        "Welcome, dear grandson, to great nation of Cobrastan! Founder? Me! President? Me! Taxpayers? You! Workforce? Also you!",
        "I got in touch with some other leaders that similarly have been oppressed by the international community, and I must say, this Kim guy is a great lad.",
        "They gave me some ideas, and I think i will follow them. Now I have a plan on how to defend my independence from the heinous United States. A strategic nuclear warhead!",
        "Now here you come in play, gotta repay those 30k I paid for your Liberal Arts degree, haha, right?",
        "I lent you this hazmat suit. And, here is a list of what I need from you (press Q). Into the mines you go.",
        "Hurry up, defending your homeland takes some work, come back when you have everything!",
        "Excellent, children of Cobrastan will learn in schools about your great efforts to defend our homeland."
        
    };

    void Start(){
        next.onClick.AddListener(ChangeText);
        dialogue.text = sentences[0];
    }

    void Update(){
        if(player.hasNuke){
            next.enabled = true;
            next.interactable = true;
        } 
    }

    void ChangeText(){
        dialogueCounter += 1;
        
        if(dialogueCounter >= 5 && !player.hasNuke){
            next.enabled = false;
            next.interactable = false;
            dialogueCounter = 5;
        }
        dialogue.text = sentences[dialogueCounter];
    }
}