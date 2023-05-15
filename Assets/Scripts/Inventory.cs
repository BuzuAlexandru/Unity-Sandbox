using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{   
    public Vector2 offset;
    public Vector2 multiplier;
    public GameObject inventoryUI;
    public GameObject inventorySlotPrefab;
    public int inventoryWidth;
    public int inventoryHeight;
    public InventorySlot[,] inventorySlots;
    public GameObject[,] uiSlots;

    private void Start(){
        inventorySlots = new InventorySlot[inventoryWidth, inventoryHeight];
        uiSlots = new GameObject[inventoryWidth, inventoryHeight];
        SetupUI();
        UpdateInventoryUI();
    }

    void SetupUI(){
        for (int x = 0; x < inventoryWidth; x++){
            for (int y = 0; y<inventoryHeight; y++){
                GameObject inventorySlot = Instantiate(inventorySlotPrefab, inventoryUI.transform.GetChild(0).transform); 
                inventorySlot.GetComponent<RectTransform>().localPosition = new Vector3((x * multiplier.x)+offset.x,(y*multiplier.y) + offset.y);
                uiSlots[x,y] = inventorySlot;
                inventorySlots[x,y] = null;
            }
        }
    }

    void UpdateInventoryUI(){
        string assetPath = "Assets/Titlesets/mainlev_build_343.asset";
        Sprite sprite = Resources.Load<Sprite>(assetPath);
        for (int x = 0; x < inventoryWidth; x++){
            for (int y = 0; y<inventoryHeight; y++){
               if (inventorySlots[x,y] == null){
                    uiSlots[x,y].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    uiSlots[x,y].transform.GetChild(0).GetComponent<Image>().enabled = false;
               }
               else {
                    uiSlots[x,y].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    uiSlots[x,y].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
               }
            }
        }
    }
}
