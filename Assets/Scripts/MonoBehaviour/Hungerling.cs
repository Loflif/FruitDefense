using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hungerling : MonoBehaviour
{
    [SerializeField] private float LerpSpeed = 2.0f;
    [SerializeField] private float WaypointProximityThreshold = 0.1f;
    [SerializeField] private float Health = 1.0f;
    [SerializeField] private float HitFlashSpeed = 0.2f;

    [SerializeField] private SpriteRenderer SR = null;

    private List<Transform> Waypoints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Waypoints.Count > 0)
        {
            MoveTowardsNextWaypoint();
        }
    }

    public void Init(List<Transform> p_Waypoints)
    {
        foreach(Transform t in p_Waypoints)
        {
            Waypoints = new List<Transform>(p_Waypoints);
        }
    }

    private void MoveTowardsNextWaypoint()
    {
        if (Vector3.Distance(transform.position, Waypoints[0].position) < WaypointProximityThreshold)
        {
            Waypoints.RemoveAt(0);
        }
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[0].position, Time.deltaTime / LerpSpeed);
    }

    public void TakeDamage(float p_Damage)
    {
        Health -= p_Damage;
        if(Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(SpriteFlasher());
        }
    }

    private void OnTriggerEnter2D(Collider2D p_Collision)
    {
        if(p_Collision.CompareTag("HungerlingDeathArea"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (Health <= 0)
        {
            Spawner.Instance.HungerlingKilled();
        }
        Spawner.Instance.AlterNumberHungerlingsAlive(-1);
        
        Destroy(gameObject);
    }

    IEnumerator SpriteFlasher()
    {

        for (float f = 1f; f >= 0; f -= HitFlashSpeed)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            SR.color = temp;
            yield return null;
        }
        for (float f = 0f; f <= 1.0f; f += HitFlashSpeed)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            SR.color = temp;
            yield return null;
        }
    }
}
