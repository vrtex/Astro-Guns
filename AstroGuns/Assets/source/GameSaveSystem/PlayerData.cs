using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
	// weapons
    public int		weaponSpawnLevel;
    public int[]	weaponsId;
	public int      biggestWeaponId;

	// money
	public double	playerCredits;
    public double	playerEther;
	public double   playerMeteors;
	public double   playerMeteorsToReset;

	// upgrades
	public int[]    upgrades;

	// reward
	public int		lastDailyReward;
	public int      lastTimeDailyReward;

	// warehouse
	public int[]    chests;
	public int[]    keys;
	public int[]    dust;
	public int      boughtPlace;

	// metalforge
	public float    fullMeltTime;
	public float    timeToEndMelt;

	// settings
	public bool     sound;
	public bool     music;

	// sklep
	public double       earningTime; //w godzinach zarabienie offline
	public double       creditMultipler;

	// statistics
	public bool isRunFirstTime;
	public int merge;

	//czas
	public long lastTimeAfterOffGame;

	public PlayerData()
    {
        // 00 level spawnowanej broni
        weaponSpawnLevel = Inventory.weaponSpawnLevel;

        // 01 poziomy broni w slotach, jeżeli nie ma, to -1
        weaponsId = new int[Inventory.SLOT_QUANTITY];
        for(int i = 0; i < Inventory.SLOT_QUANTITY; i++)
        {
            if(Inventory.slots[i].weapon != null)
            {
                weaponsId[i] = Inventory.slots[i].weapon.id;
            }
            else
            {
                weaponsId[i] = -1;
            }
        }

        // 02 stan kredytów
        playerCredits = MoneyPocket.Instance.Money.ActualValue;

        // 03 stan eteru
        playerEther = MoneyPocket.Instance.Ether.ActualValue;

		// 04 stan ulepszeń
		upgrades = new int[UpgradesManager.Manager.Upgrades.Count];
		for(int i = 0; i < UpgradesManager.Manager.Upgrades.Count; ++i)
		{
			upgrades[i] = UpgradesManager.Manager.Upgrades[i].CurrentLevel;
		}

		// 05 godzina o której wyłączyliśmy grę // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		lastTimeAfterOffGame = DateTimeSystem.Instance.lastTimeAfterOffGame;

		// 06 czas do stworzenia klucza // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		fullMeltTime = WarehouseManager.Instance.fullMeltTime;
		timeToEndMelt = WarehouseManager.Instance.timeToEndMelt;

		// 07 który dzień z rzędu odbieramy nagrodę dnia // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		lastDailyReward = DateTimeSystem.Instance.lastDailyReward;
		lastTimeDailyReward = DateTimeSystem.Instance.lastTimeDailyReward;

		// 08 skrzynie
		chests = new int[WarehouseManager.SIZE];
		for(int i = 0; i < WarehouseManager.SIZE; ++i)
		{
			chests[i] = WarehouseManager.Instance.chests[i];
		}

		// 09 klucze
		keys = new int[WarehouseManager.KEYS];
		for(int i = 0; i < WarehouseManager.KEYS; ++i)
		{
			keys[i] = WarehouseManager.Instance.keysAmount[i];
		}

		// 10 fragmenty kluczy
		dust = new int[WarehouseManager.KEYS];
		for(int i = 0; i < WarehouseManager.KEYS; ++i)
		{
			dust[i] = WarehouseManager.Instance.dustAmount[i];
		}

		// 11 rdzenie energetyczne

		// 12 meteory
		playerMeteors = MoneyPocket.Instance.Meteor.ActualValue;
		playerMeteorsToReset = MoneyPocket.Instance.MeteorToReset.ActualValue;

		// 13 kupione sloty w magazynie skrzynek
		boughtPlace = WarehouseManager.Instance.boughtPlace;

		// 14 muzyka i dźwięki
		sound = AudioManager.Instance.soundsOn;
		music = AudioManager.Instance.musicOn;

		//15 aktualnie najwyższy poziom broni
		biggestWeaponId = InventorySystem.Instance.biggestWeaponId;

		//16 wykupione mnożniki pieniędzy
		creditMultipler = ItemShop.Instance.creditMultipler;

		//17 wykupiona ilość czasu zarabiania podczas bycia offline
		earningTime = ItemShop.Instance.earningTime;

		// 18 statystyki
		isRunFirstTime = StatisticsManager.Instance.isRunFirstTime;
		merge = StatisticsManager.Instance.merge;
	}

	public void Reset()
    {

        // 00 level spawnowanej broni
        weaponSpawnLevel = 1;

        // 01 poziomy broni w slotach, jeżeli nie ma, to -1
        weaponsId = new int[Inventory.SLOT_QUANTITY];
        for(int i = 0; i < Inventory.SLOT_QUANTITY; i++)
        {
            weaponsId[i] = -1;
        }

        // 02 stan kredytów
        playerCredits = 0;

        // 03 stan eteru
        playerEther = 0;

		// 04 stan ulepszeń
		upgrades = new int[UpgradesManager.Manager.Upgrades.Count];
		for(int i = 0; i < UpgradesManager.Manager.Upgrades.Count; ++i)
		{
			upgrades[i] = 0;
		}

		// 05 godzina o której wyłączyliśmy grę // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		lastTimeAfterOffGame = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

		// 06 czas do stworzenia klucza // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		fullMeltTime = 0f;
		timeToEndMelt = 0f;

		// 07 który dzień z rzędu odbieramy nagrodę dnia // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
		lastDailyReward = -1;
        lastTimeDailyReward = -1;

		// 08 skrzynie
		chests = new int[WarehouseManager.SIZE];
		for(int i = 0; i < WarehouseManager.SIZE; ++i)
		{
			if(i > 11 + boughtPlace) chests[i] = -1;
			else chests[i] = 0;
		}

		// 09 klucze
		keys = new int[WarehouseManager.KEYS];
		for(int i = 0; i < WarehouseManager.KEYS; ++i)
		{
			keys[i] = 0;
		}

		// 10 fragmenty kluczy
		dust = new int[WarehouseManager.KEYS];
		for(int i = 0; i < WarehouseManager.KEYS; ++i)
		{
			dust[i] = 0;
		}

		// 11 rdzenie energetyczne

		// 12 meteory
		playerMeteors = 0;
		playerMeteorsToReset = 0;

		// 13 kupione sloty w magazynie skrzynek
		boughtPlace = 0;

		// 14 muzyka i dźwięki
		sound = true;
		music = true;

		//15 aktualnie najwyższy poziom broni
		biggestWeaponId = 0;

		//16 wykupione mnożniki pieniędzy
		creditMultipler = 0;

		//17 wykupiona ilość czasu zarabiania podczas bycia offline
		earningTime = 3;

		// 18 statystyki
		isRunFirstTime = false;
		merge = 0;
	}

    public static void ApplyPlayerData(PlayerData data)
    {
        if (data != null)
        {
            // 00 level spawnowanej broni
            Inventory.weaponSpawnLevel = data.weaponSpawnLevel;

            // 01 poziomy broni w slotach, jeżeli nie ma, to -1
            for (int i = 0; i < Inventory.SLOT_QUANTITY; i++)
            {
                WeaponSpawner.setWeaponData(i, data.weaponsId[i]);
                WeaponSpawner.resetWeaponView(i);
            }

            // 02 stan kredytów
            MoneyPocket.Instance.Money.ActualValue = data.playerCredits;

            // 03 stan eteru
            MoneyPocket.Instance.Ether.ActualValue = data.playerEther;

			// 04 stan ulepszeń
			if(data.upgrades == null)
			{
				data.upgrades = new int[UpgradesManager.Manager.Upgrades.Count];
			}
			for(int i = 0; i < data.upgrades.Length; ++i)
			{
				UpgradesManager.Manager.Upgrades[i].SetLevel(data.upgrades[i]);
			}

			// 05 godzina o której wyłączyliśmy grę
			DateTimeSystem.Instance.lastTimeAfterOffGame = data.lastTimeAfterOffGame;

			// 06 czas do stworzenia klucza
			WarehouseManager.Instance.timeToEndMelt = data.timeToEndMelt;

			long diffTime = DateTimeSystem.Instance.lastTimeAfterOffGame - DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			if(data.fullMeltTime <= (float)diffTime) WarehouseManager.Instance.fullMeltTime = 0f;
			else WarehouseManager.Instance.fullMeltTime = data.fullMeltTime - (float)diffTime;

			// 07 który dzień z rzędu odbieramy nagrodę dnia // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie
			DateTimeSystem.Instance.lastDailyReward = data.lastDailyReward;
			DateTimeSystem.Instance.lastTimeDailyReward = data.lastTimeDailyReward;

			// 08 skrzynie
			WarehouseManager.Instance.chests = new int[WarehouseManager.SIZE];
			if(data.chests == null)
			{
				data.chests = new int[WarehouseManager.SIZE];
				for(int i = 0; i < WarehouseManager.SIZE; ++i)
				{
					if(i > 11 + data.boughtPlace) data.chests[i] = -1;
					else data.chests[i] = 0;
				}
			}
			for(int i = 0; i < WarehouseManager.SIZE; ++i)
			{
				WarehouseManager.Instance.chests[i] = data.chests[i];
			}

			// 09 klucze
			WarehouseManager.Instance.keysAmount = new int[WarehouseManager.KEYS];
			if(data.keys == null)
			{
				data.keys = new int[WarehouseManager.KEYS];
				for(int i = 0; i < WarehouseManager.KEYS; ++i)
				{
					data.keys[i] = 0;
				}
			}
			for(int i = 0; i < WarehouseManager.KEYS; ++i)
			{
				WarehouseManager.Instance.keysAmount[i] = data.keys[i];
			}

			// 10 fragmenty kluczy
			WarehouseManager.Instance.dustAmount = new int[WarehouseManager.KEYS];
			if(data.dust == null)
			{
				data.dust = new int[WarehouseManager.KEYS];
				for(int i = 0; i < WarehouseManager.KEYS; ++i)
				{
					data.dust[i] = 0;
				}
			}
			for(int i = 0; i < WarehouseManager.KEYS; ++i)
			{
				WarehouseManager.Instance.dustAmount[i] = data.dust[i];
			}

			// 11 rdzenie energetyczne

			// 12 meteory
			MoneyPocket.Instance.Meteor.ActualValue = data.playerMeteors;
			MoneyPocket.Instance.MeteorToReset.ActualValue = data.playerMeteorsToReset;

			// 13 kupione sloty w magazynie skrzynek
			WarehouseManager.Instance.boughtPlace = data.boughtPlace;

			// 14 muzyka i dźwięki
			AudioManager.Instance.soundsOn = data.sound;
			AudioManager.Instance.musicOn = data.music;

			//15 aktualnie najwyższy poziom broni
			InventorySystem.Instance.biggestWeaponId = data.biggestWeaponId;

			//16 wykupione mnożniki pieniędzy
			ItemShop.Instance.creditMultipler = data.creditMultipler;

			//17 wykupiona ilość czasu zarabiania podczas bycia offline
			ItemShop.Instance.earningTime = data.earningTime;

			// 18 statystyki
			StatisticsManager.Instance.isRunFirstTime = data.isRunFirstTime;
			StatisticsManager.Instance.merge = data.merge;


			// - NAN - do testów
			WarehouseManager.Instance.chests[0] = 1;
			WarehouseManager.Instance.keysAmount[0] = 2;
			WarehouseManager.Instance.dustAmount[0] = 4;
			WarehouseManager.Instance.dustAmount[1] = 4;
		}
	}
}
