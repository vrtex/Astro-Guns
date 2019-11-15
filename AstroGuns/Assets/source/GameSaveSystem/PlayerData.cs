﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int weaponSpawnLevel;
    public int[] weaponsId;

    public double playerCredits;
    public double playerEther;

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

        // 05 czas do stworzenia klucza // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

        // 06 godzina o której wyłączyliśmy grę // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

        // 07 który dzień z rzędu odbieramy nagrodę dnia // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

        // 08 skrzynie

        // 09 klucze

        // 10 fragmenty kluczy

        // 11 rdzenie energetyczne

        // 12 meteory

        // 13 kupione sloty w magazynie skrzynek
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

            // 05 czas do stworzenia klucza // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

            // 06 godzina o której wyłączyliśmy grę // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

            // 07 który dzień z rzędu odbieramy nagrodę dnia // trzeba obgadać, żeby ktoś nie zmieniał daty w fonie

            // 08 skrzynie

            // 09 klucze

            // 10 fragmenty kluczy

            // 11 rdzenie energetyczne

            // 12 meteory

            // 13 kupione sloty w magazynie skrzynek
        }
    }
}
