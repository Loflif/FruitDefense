using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyManager : MonoBehaviour
{
    private static MoneyManager _instance;
    public static MoneyManager Instance
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

    [SerializeField] private Text MoneyText;
    [SerializeField] private int StartMoney = 50;
    [SerializeField] public int MoneyPerHungerling = 1; 

    public int MoneyCount;

    void Start()
    {
        MoneyCount = StartMoney;
        MoneyText.text = MoneyCount.ToString() + "$";
    }

    void Update()
    {
        
    }

    public void AlterMoneySum(int p_Change)
    {
        MoneyCount += p_Change;
        MoneyText.text = MoneyCount.ToString() + "$";
    }
}
