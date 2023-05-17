using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{   
    public Vector2 offset;
    public Vector2 offset2;
    public Vector2 multiplier;
    public GameObject inventoryUI;
    public GameObject hotbarUI;
    public GameObject inventorySlotPrefab;
    public GameObject inventorySlotPrefab2;
    public int inventoryWidth;
    public int inventoryHeight;
    public InventorySlot[,] inventorySlots;
    public GameObject[] hotbarUISlots;
    public GameObject[,] uiSlots;
    public InventorySlot[] hotbarSlots;

    private void Start()
    {
        inventorySlots = new InventorySlot[inventoryWidth, inventoryHeight];
        uiSlots = new GameObject[inventoryWidth, inventoryHeight];
        hotbarSlots = new InventorySlot[inventoryWidth];
        hotbarUISlots = new GameObject[inventoryWidth];
        SetupUI();
        UpdateInventoryUI();
    }

    void SetupUI()
    {
        //setup inventory
        for (int x = 0; x < inventoryWidth; x++)
        {
            for (int y = 0; y < inventoryHeight; y++)
            {
                GameObject inventorySlot = Instantiate(inventorySlotPrefab, inventoryUI.transform.GetChild(0).transform); 
                inventorySlot.GetComponent<RectTransform>().localPosition = new Vector3((x * multiplier.x) + offset.x, (y * multiplier.y) + offset.y);
                uiSlots[x, y] = inventorySlot;
                if ((x==0 && y==3) || (x==1 && y==3)){
                    inventorySlots[x, y] = ScriptableObject.CreateInstance<InventorySlot>();
                    inventorySlots[x,y].quantity = 6969;
                }
                if ((x>1 && x<9 && y==3) || (x>=0 && x<6 && y==2)){ 
                    inventorySlots[x, y] = ScriptableObject.CreateInstance<InventorySlot>();
                    inventorySlots[x,y].quantity = 0;
                }
            }
        }

        //setup hotbar
        for (int x = 0; x < inventoryWidth; x++)
        {
            GameObject hotbarSlot = Instantiate(inventorySlotPrefab2, hotbarUI.transform); 
            hotbarSlot.GetComponent<RectTransform>().localPosition = new Vector3((x * multiplier.x) + offset2.x, offset2.y);
            hotbarUISlots[x] = hotbarSlot;
            
        }


    }

    public void UpdateInventoryUI()
{   
    //update inventory
    string gunPath = "Inventory/image_2023-05-16_16-05-48";
    string wandPath = "Inventory/wand";
    string uraniumPath = "Inventory/uranium";
    string titaniumPath = "Inventory/titanium";
    string aluminumPath = "Inventory/aluminum";
    string goldPath = "Inventory/gold";
    string berylliumPath = "Inventory/beryllium";
    string lithiumPath = "Inventory/lithium";
    string plutoniumPath = "Inventory/plutonium";
    string neutroninitiatorPath = "Inventory/neutroninitiator";
    string lithiumdeuteridePath = "Inventory/lithiumdeuteride";
    string hardmetalcasingPath = "Inventory/hardmetalcasing";
    string radioactivecorePath = "Inventory/radioactivecore";
    string explosivelensesPath = "Inventory/explosivelenses";
    string mysterydevicePath = "Inventory/bomb";
    Sprite sprite = Resources.Load<Sprite>(gunPath);
    Sprite sprite2 = Resources.Load<Sprite>(wandPath);
    Sprite sprite3 = Resources.Load<Sprite>(uraniumPath);
    Sprite sprite4 = Resources.Load<Sprite>(titaniumPath);
    Sprite sprite5 = Resources.Load<Sprite>(aluminumPath);
    Sprite sprite6 = Resources.Load<Sprite>(goldPath);
    Sprite sprite7 = Resources.Load<Sprite>(berylliumPath);
    Sprite sprite8 = Resources.Load<Sprite>(lithiumPath);
    Sprite sprite9 = Resources.Load<Sprite>(plutoniumPath);
    Sprite sprite10 = Resources.Load<Sprite>(neutroninitiatorPath);
    Sprite sprite11 = Resources.Load<Sprite>(lithiumdeuteridePath);
    Sprite sprite12 = Resources.Load<Sprite>(hardmetalcasingPath);
    Sprite sprite13 = Resources.Load<Sprite>(radioactivecorePath);
    Sprite sprite14 = Resources.Load<Sprite>(explosivelensesPath);
    Sprite sprite15 = Resources.Load<Sprite>(mysterydevicePath);
    for (int x = 0; x < inventoryWidth; x++)
    {
        for (int y = 0; y < inventoryHeight; y++)
        {
            if (inventorySlots[x, y] == null)
            {
                uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = null;
                uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().enabled = false;

                uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>().text = "0";
                uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>().enabled = false;
                //Debug.Log(uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>());
            }
            else
            if (inventorySlots[x,y].quantity == 6969){
                uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().enabled = true;
                if (x==0)
                uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                else uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
                uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>().enabled = false;
            }
            else
            {
                uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().enabled = true;
                if (x == 2 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
                if (x == 3 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
                if (x == 4 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
                if (x == 5 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
                if (x == 6 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite7;
                if (x == 7 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite8;
                if (x == 8 && y==3)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite9;
                if (x == 0 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite10;
                if (x == 1 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite11;
                if (x == 2 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite12;
                if (x == 3 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite13;
                if (x == 4 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite14;
                if (x == 5 && y==2)
                    uiSlots[x, y].transform.GetChild(0).GetComponent<Image>().sprite = sprite15;    
                uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>().text = inventorySlots[x,y].quantity.ToString();
                uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>().enabled = true;
            }
        }
    }

    //update hotbar
    for (int x = 0; x < inventoryWidth; x++)
    {
        if (inventorySlots[x, inventoryHeight-1] == null)
        {
            hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = null;
            hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().enabled = false;

            hotbarUISlots[x].transform.GetChild(1).GetComponent<TMP_Text>().text = "0";
            hotbarUISlots[x].transform.GetChild(1).GetComponent<TMP_Text>().enabled = false;
            //Debug.Log(uiSlots[x, y].transform.GetChild(1).GetComponent<TMP_Text>());
        }
        else
        if (inventorySlots[x,inventoryHeight-1].quantity == 6969){
            hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().enabled = true;
            if (x==0)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                else hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
            hotbarUISlots[x].transform.GetChild(1).GetComponent<TMP_Text>().enabled = false;
        }
        else
        {
            hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().enabled = true;
            if (x == 2)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
            if (x == 3)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
            if (x == 4)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
            if (x == 5)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
            if (x == 6)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite7;
            if (x == 7)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite8;
            if (x == 8)
                hotbarUISlots[x].transform.GetChild(0).GetComponent<Image>().sprite = sprite9;
            hotbarUISlots[x].transform.GetChild(1).GetComponent<TMP_Text>().text = inventorySlots[x,inventoryHeight-1].quantity.ToString();
            hotbarUISlots[x].transform.GetChild(1).GetComponent<TMP_Text>().enabled = true;
        }
        
    }

}

}
