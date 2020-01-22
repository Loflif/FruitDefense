using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private CircleCollider2D Collider;
    [SerializeField] private ParticleSystem Explosion;

    private List<GameObject> ObjectsInRange = new List<GameObject>();

    private FruitData FruitData;

    private float FuseTimer = 5.0f;
    private float Damage;

    void Start()
    {
        StartCoroutine(Pulsate());
    }

    void Update()
    {
        UpdateTimers();
        if(FuseTimer <= 0)
        {
            Explode();
        }
    }

    private void UpdateTimers()
    {
        FuseTimer -= Time.deltaTime;
    }

    private void ClearEmptySlots()
    {
        ObjectsInRange.RemoveAll(x => x == null);
    }

    private void Explode()
    {
        StopAllCoroutines();
        SR.enabled = false;
        Explosion = Instantiate(Explosion, transform.position, transform.rotation);
        var tmp = Explosion.main;
        tmp.startSize = Collider.radius * 2.4f;
        ClearEmptySlots();
        for(int i = 0; i < ObjectsInRange.Count; i++)
        {
            ObjectsInRange[i].SendMessage("TakeDamage", Damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D p_Collision)
    {
        if (p_Collision.CompareTag("Hungerling"))
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

    public void SetFruitData(FruitData p_Fruitdata)
    {
        
        FruitData = p_Fruitdata;
        if(FruitData.SeasonStrength[SeasonController.Instance.SeasonIterator])
        {
            Collider.radius = FruitData.BombRadius * FruitData.SeasonStrengthMultiplier;
        }
        else
        {
            Collider.radius = FruitData.BombRadius;
        }
        SR.sprite = FruitData.Sprite;
        FuseTimer = FruitData.BombFuseTimer;
        Damage = FruitData.BombExplosionDamage;
    }

    IEnumerator Pulsate()
    {
        const float COLOR_PULSE_SPEED = 0.03f;
        while (true)
        {
            for (float f = 1.0f; f >= 0; f -= COLOR_PULSE_SPEED)
            {
                Color temp = SR.color;
                temp.b = f;
                temp.g = f;
                SR.color = temp;
                transform.localScale += new Vector3(f / 1000, f / 1000, f / 1000);
                yield return null;
            }
            for (float f = 0.0f; f <= 1.0f; f += COLOR_PULSE_SPEED)
            {
                Color temp = SR.color;
                temp.b = f;
                temp.g = f;
                SR.color = temp;
                transform.localScale -= new Vector3(f / 1000, f / 1000, f / 1000);
                yield return null;
            }
        }
    }
}
