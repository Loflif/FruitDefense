using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanonState
{
    ENEMY_NOT_FOUND,
    ENEMY_FOUND
}
public class Canon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR = null;
    [SerializeField] private BoxCollider2D Collider = null;

    private FruitData FruitData = null;

    private List<GameObject> ObjectsInRange = new List<GameObject>();

    private GameObject Shot = null;
    private float FireRate = 0.0f;
    private float ShootCooldown = 0.0f;

    private CanonState State = CanonState.ENEMY_NOT_FOUND;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
        if(State == CanonState.ENEMY_FOUND
            && ShootCooldown <= 0)
        {
            Shoot();
        }
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        ShootCooldown -= Time.deltaTime;
    }

    private void CheckForEnemies()
    {
        ObjectsInRange.RemoveAll(x => x == null);
        if(ObjectsInRange.Count > 0)
        {
            State = CanonState.ENEMY_FOUND;
        }
        else
        {
            State = CanonState.ENEMY_NOT_FOUND;
        }
    }

    private void Shoot()
    {
        Instantiate(Shot, transform);
        ShootCooldown = FireRate;
    }

    public void SetFruitData(FruitData p_Fruitdata)
    {
        FruitData = p_Fruitdata;
        SR.sprite = FruitData.Sprite;
        if (FruitData.SeasonStrength[SeasonController.Instance.SeasonIterator])
        {
            FireRate = FruitData.FireRate / FruitData.SeasonStrengthMultiplier;
        }
        else
        {
            FireRate = FruitData.FireRate;
        }
        Shot = FruitData.Shot;
    }

    private void OnTriggerEnter2D(Collider2D p_Collision)
    {
        if(p_Collision.CompareTag("Hungerling"))
        {
            ObjectsInRange.Add(p_Collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D p_Collision)
    {
        if (p_Collision.CompareTag("Hungerling"))
        {
            ObjectsInRange.RemoveAt(ObjectsInRange.IndexOf(p_Collision.gameObject));
        }
    }
}
