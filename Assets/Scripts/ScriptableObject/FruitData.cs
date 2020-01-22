using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    SHOOTER,
    SLAPPER,
    BOMB
}

[CreateAssetMenu(fileName = "newFruitData", menuName = "FruitData")]
public class FruitData : ScriptableObject
{
    public bool[] SeasonStrength = new bool[4];
    public int[] CostPerSeason = new int[4];
    public float SeasonStrengthMultiplier;
    public TowerType Type;
    public string Name;
    public GameObject Shot;
    public SlapperDown SlapperDown;
    public float FireRate;
    public float BombExplosionDamage;
    public float BombFuseTimer;
    public float BombRadius;
    public Sprite Sprite;
}
