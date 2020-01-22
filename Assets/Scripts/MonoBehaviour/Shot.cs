using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float Velocity = 5.0f;
    [SerializeField] private float TravelMax = 5.0f;
    [SerializeField] private float Damage = 0.5f;
    [SerializeField] private int MaximumHits = 2;

    private float Health = 1.0f;
    private float DamageTakenPerHit;
    private float DistanceTravelled = 0.0f;

    void Start()
    {
        DamageTakenPerHit = Health / MaximumHits;
    }

    void Update()
    {
        transform.position += transform.right * Velocity * Time.deltaTime;
        DistanceTravelled += Velocity * Time.deltaTime;
        if(DistanceTravelled >= TravelMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D p_Collision)
    {
        if (p_Collision.gameObject.CompareTag("Hungerling"))
        {
            p_Collision.gameObject.SendMessage("TakeDamage", Damage);
            TakeDamage(DamageTakenPerHit);
        }
    }

    private void TakeDamage(float p_Damage)
    {
        Health -= p_Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
