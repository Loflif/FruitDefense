using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private FruitData FruitData;
    [SerializeField] private int TowerCount;
    [SerializeField] private Image TowerImage;
    [SerializeField] private Text CountText;
    [SerializeField] private int SlotNumber;


    public void OccupySlot(FruitData p_FruitData)
    {
        FruitData = p_FruitData;
        SetTowerImage(p_FruitData.Sprite);
        TowerImage.enabled = true;
        AlterTowerCount(+1);
    }

    public void ClearSlot()
    {
        TowerImage.sprite = null;
        FruitData = null;
        TowerImage.enabled = false;
        CountText.enabled = false;
    }

    public FruitData GetFruitData()
    {
        return FruitData;
    }

    public void AlterTowerCount(int Amount)
    {
        if (FruitData == null)
            return;
        if (TowerCount == 0)
            CountText.enabled = true;
        TowerCount += Amount;
        CountText.text = TowerCount.ToString();
        if (TowerCount <= 0)
            ClearSlot();
    }

    public int GetTowerCount()
    {
        return TowerCount;
    }

    public void SetTowerImage(Sprite p_Sprite)
    {
        TowerImage.sprite = p_Sprite;
    }

    public Image GetTowerImage()
    {
        return TowerImage;
    }

    public int GetSlotNumber()
    {
        return SlotNumber;
    }
}
