using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Manager { get; private set; }
    public WeaponSpawner Spawner;
    public List<Upgrade> Upgrades = new List<Upgrade>()
    {
        new SpawnLevelUpgrade(),
        new SpawnTimeUpgrade(),
        new MergeEtherChance(),
        new DoubleSpawnUpgrade(),
        new SpawnHigherUpgrade()
    };

	private void Awake()
	{
		Manager = this;
		Upgrade.Spawner = Spawner;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    public Upgrade GetUpgrade(int index)
    {
        if(index > Upgrades.Count - 1)
            return null;
        return Upgrades[index];
    }
}
