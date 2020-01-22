using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private const int InventorySlotMax = 4;
    [SerializeField] public List<InventorySlot> InventorySlots = new List<InventorySlot>();

    public void AddItem(FruitData p_FruitData)
    {
        if (p_FruitData.CostPerSeason[SeasonController.Instance.SeasonIterator] > MoneyManager.Instance.MoneyCount)
            return;
        for (int i = 0; i < InventorySlots.Count; i++) //Already have that item
        {
            if (InventorySlots[i].GetFruitData() == null)
                continue;
            if (InventorySlots[i].GetFruitData().Name == p_FruitData.Name)
            {
                InventorySlots[i].AlterTowerCount(+1);
                return;
            }
        }
        for (int i = 0; i < InventorySlots.Count; i++) //Empty Slot
        {
            if (InventorySlots[i].GetFruitData() == null)
            {
                InventorySlots[i].OccupySlot(p_FruitData);
                return;
            }
        }
    }
    public void RemoveItem(int InventorySlotNumber)
    {
        InventorySlots[InventorySlotNumber].AlterTowerCount(-1);
    }

    public void RemoveItem(FruitData p_FruitData)
    {
        for (int i = 0; i < InventorySlots.Count; i++) //Already have that item
        {
            if (InventorySlots[i].GetFruitData() == null)
                continue;
            if (InventorySlots[i].GetFruitData().Name == p_FruitData.Name)
            {
                InventorySlots[i].AlterTowerCount(-1);
                return;
            }
        }
    }

    public bool ItemRemaining(FruitData p_FruitData)
    {
        for(int i = 0; i< InventorySlots.Count; i++)
        {
            if (InventorySlots[i].GetFruitData() == null)
                continue;
            if(InventorySlots[i].GetFruitData().Name == p_FruitData.Name)
            {
                return true;
            }
        }
        return false;
    }
}
