using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{   
    public Player player;
    void updateButton1(){
        if (player.inventory.inventorySlots[5,3].quantity >= 1 && player.inventory.inventorySlots[6,3].quantity >= 1){
            player.inventory.inventorySlots[5,3].quantity-=1;
            player.inventory.inventorySlots[0,2].quantity+=1;
        }
    }
}
