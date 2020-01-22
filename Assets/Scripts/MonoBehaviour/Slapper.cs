using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private float DownTime = 0.2f;

    private SpriteRenderer DownSR;

    private FruitData FruitData;

    private SlapperDown SlapperDown;

    private float SlapRate;
    private float SlapCooldown = 0.0f;
    private float DownTimer = 0.0f;

    void Start()
    {
        SlapperDown = Instantiate(SlapperDown, transform);
        DownSR = SlapperDown.GetComponent<SpriteRenderer>();
        DownSR.enabled = false;
    }

    void Update()
    {
        if(SlapperDown.AreEnemiesInRange()
            && SlapCooldown <= 0)
        {
            Slap();
        }
        else if(DownTimer <= 0)
        {
            UnSlap();
        }
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        SlapCooldown -= Time.deltaTime;
        DownTimer -= Time.deltaTime;
    }

    private void Slap()
    {
        SR.enabled = false;
        DownSR.enabled = true;
        SlapCooldown = SlapRate;
        DownTimer = DownTime;
        SlapperDown.DamageEnemiesInRange();
    }

    private void UnSlap()
    {
        SR.enabled = true;
        DownSR.enabled = false;
    }

    public void SetFruitData(FruitData p_Fruitdata)
    {
        FruitData = p_Fruitdata;
        SR.sprite = FruitData.Sprite;
        SlapperDown = FruitData.SlapperDown;
        if (FruitData.SeasonStrength[SeasonController.Instance.SeasonIterator])
        {
            SlapRate = FruitData.FireRate / FruitData.SeasonStrengthMultiplier;
        }
        else
        {
            SlapRate = FruitData.FireRate;
        }
    }
}
