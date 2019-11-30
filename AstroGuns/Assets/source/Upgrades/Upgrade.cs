using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Upgrade
{
    public static WeaponSpawner Spawner;
    public int CurrentLevel { get; protected set; } = 1;
    public int MaxLevel = 50;
    public bool IsMaxLevel { get => CurrentLevel == MaxLevel; }
    public abstract string GetDescription();
    public abstract double GetUpgradeCost();

    public UnityEvent OnUpgrade = new UnityEvent();

    public virtual void Increase()
    {
        CurrentLevel++;
        OnUpgrade.Invoke();
    }

	public virtual void SetLevel(int lvl)
	{
		CurrentLevel = lvl;
		OnUpgrade.Invoke();
	}

}
