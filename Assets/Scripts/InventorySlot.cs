using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySlot", menuName = "ScriptableObjects/InventorySlot")]
public class InventorySlot : ScriptableObject
{
    public Vector2Int position;
    public int quantity;
}

