using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCost : MonoBehaviour
{
    [SerializeField] private Text CostText;
    [SerializeField] private FruitData FruitData;

    private int Cost;

    void Start()
    {

    }

    void Update()
    {
        Cost = FruitData.CostPerSeason[SeasonController.Instance.SeasonIterator];
        CostText.text = FruitData.CostPerSeason[SeasonController.Instance.SeasonIterator].ToString() + "$";
    }

    public void BuyItem()
    {
        if (FruitData.CostPerSeason[SeasonController.Instance.SeasonIterator] > MoneyManager.Instance.MoneyCount)
            return;
        MoneyManager.Instance.AlterMoneySum(-Cost);
    }

    public void SellItem()
    {
        MoneyManager.Instance.AlterMoneySum(+Cost);
    }
}
