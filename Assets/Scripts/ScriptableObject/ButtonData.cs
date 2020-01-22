using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newButtonData", menuName = "ButtonData")]
public class ButtonData : ScriptableObject
{
    public string Name;
    public ScriptableObject TowerData;
    public TowerType TowerType;
}
