using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum GameState
    {
        SHOPPING,
        PLACING,
        RUNNING
    }

    public static GameState State = GameState.SHOPPING;

    [SerializeField] private Canvas ShoppingCanvas = null;
    [SerializeField] private Spawner Spawner = null;
    [SerializeField] private Inventory Inventory = null;
    [SerializeField] private PlacementObject PlacementObject = null;
    [SerializeField] private Button ButtonStart = null;
    [SerializeField] private Canvas PostGameCanvas = null;
    [SerializeField] private PostGameText PostGameText = null;
    [SerializeField] private GameObject PlacementTileHolder = null;

    private List<Transform> PlacementTiles = new List<Transform>();

    private static Game _instance;
    public static Game Instance
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
    void Start()
    {
        foreach (Transform child in PlacementTileHolder.transform)
        {
            PlacementTiles.Add(child);
        }
        PlacementObject = Instantiate(PlacementObject);
    }

    void Update()
    {
         CheckInput();
    }

    public void SelectPlacementObject(FruitData p_FruitData)
    {
        PlacementObject.SelectObject(p_FruitData);
    }

    private void CheckInput()
    {
        if(PlacementObject.GetFruitData() != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (PlacementObject.GetFruitData().Type == TowerType.BOMB)
                {
                    PlacementObject.PlaceSelectedObject();
                }
                else
                {
                    PlacementObject.LockPlacement();
                }
            }
            else if (Input.GetMouseButtonUp(0)
                && PlacementObject.GetState() == PlacementObject.PlacementObjectState.ROTATING
                && PlacementObject.GetFruitData().Type != TowerType.BOMB)
            {
                PlacementObject.PlaceSelectedObject();
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            PlacementObject.UnSelectObject();
        }
    }

    public void StartPlacing()
    {
        foreach(Transform t in PlacementTiles)
        {
            t.tag = "PlaceableTile";
        }
        ShoppingCanvas.enabled = false;
        State = GameState.PLACING;
    }

    public void StartGame()
    {
        Spawner.StartStartSpawner();
        State = GameState.RUNNING;
        ButtonStart.enabled = false;
        ButtonStart.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        PostGameCanvas.gameObject.SetActive(true);
        int MoneyGained = MoneyManager.Instance.MoneyPerHungerling * Spawner.HungerlingsKilled;
        MoneyManager.Instance.AlterMoneySum(MoneyGained);
        PostGameText.ChangeText(Spawner.HungerlingsKilled, Spawner.HungerlingSpawnCount, MoneyGained);
    }

    public void BackToShop()
    {
        PostGameCanvas.gameObject.SetActive(false);
        PlacementObject.ClearTable();
        State = GameState.SHOPPING;
        SeasonController.Instance.NextSeason();
        Spawner.NextGame();
        ButtonStart.gameObject.SetActive(true);
        ButtonStart.enabled = true;
        ShoppingCanvas.enabled = true;
    }

    public void StartButton()
    {
        if(State == GameState.SHOPPING)
        {
            StartPlacing();
        }
        else
        {
            StartGame();
        }
    }

    public void InventoryButton(int InventorySlotNumber)
    {
        if(State == GameState.SHOPPING)
        {
            if(Inventory.InventorySlots[InventorySlotNumber].GetFruitData() != null)
            {
                MoneyManager.Instance.AlterMoneySum(Inventory.InventorySlots[InventorySlotNumber].GetFruitData().CostPerSeason[SeasonController.Instance.SeasonIterator]);
            }
            Inventory.RemoveItem(InventorySlotNumber);
        }
        else
        {
            SelectPlacementObject(Inventory.InventorySlots[InventorySlotNumber].GetFruitData());
        }
    }
}
