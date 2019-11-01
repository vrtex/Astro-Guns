using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon")]
public class WeaponObject : ScriptableObject
{
    public Sprite sprite;
    public double value {
        get { return System.Math.Pow(2, id); }
    }
    public int id;
}