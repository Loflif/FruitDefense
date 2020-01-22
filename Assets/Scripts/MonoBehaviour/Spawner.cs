using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner _instance;
    public static Spawner Instance
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

    [SerializeField] public int HungerlingSpawnCount = 50;
    [SerializeField] private Hungerling Hungerling = null;
    [SerializeField] private GameObject Waypoints = null;
    [SerializeField] private float SpawnWaitTime = 0.2f;

    private List<Transform> WaypointList = new List<Transform>();

    private float HungerlingsAlive = 0;
    public int HungerlingsKilled = 0;

    void Start()
    {
        foreach (Transform child in Waypoints.transform)
        {
            WaypointList.Add(child);
        }
        HungerlingsAlive = HungerlingSpawnCount;
    }

    void Update()
    {

    }

    public float GetPercentHungerlingsAlive()
    {
        return HungerlingsAlive / HungerlingSpawnCount;
    }

    public void StartStartSpawner()
    {
        StartCoroutine(StartSpawner());
    }

    public void AlterNumberHungerlingsAlive(int p_i)
    {
        HungerlingsAlive += p_i;
        SeasonController.Instance.UpdateSliderValue(1 - GetPercentHungerlingsAlive());
        if(HungerlingsAlive == 0)
        {
            Game.Instance.GameOver();
        }
    }

    public void HungerlingKilled()
    {
        HungerlingsKilled++;
    }

    public void NextGame()
    {
        HungerlingsAlive = HungerlingSpawnCount;
        HungerlingsKilled = 0;
    }

    IEnumerator StartSpawner()
    {
        int SpawnCount = HungerlingSpawnCount;

        for(int i = 0; i < SpawnCount; i++)
        {
            Hungerling newHungerling = Instantiate(Hungerling, gameObject.transform);
            newHungerling.Init(WaypointList);
            yield return new WaitForSeconds(SpawnWaitTime);
        }
        yield return null;
    }
}
