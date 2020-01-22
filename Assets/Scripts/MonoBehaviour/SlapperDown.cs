using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapperDown : MonoBehaviour
{
    [SerializeField] float Damage = 0.5f;
    private List<GameObject> ObjectsInRange = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void ClearEmptySlots()
    {
        ObjectsInRange.RemoveAll(x => x == null);
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
    
    public bool AreEnemiesInRange()
    {
        ObjectsInRange.RemoveAll(x => x == null);
        if (ObjectsInRange.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DamageEnemiesInRange()
    {
        ObjectsInRange.RemoveAll(x => x == null);
        for(int i = 0; i < ObjectsInRange.Count; i++)
        {
            ObjectsInRange[i].SendMessage("TakeDamage", Damage);
        }
    }
}
