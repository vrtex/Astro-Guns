using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public const int ROWS = 6;
    public const int COLS = 4;
    public const int SLOT_QUANTITY = ROWS * COLS;

    public static List<Slot> slots = new List<Slot>(SLOT_QUANTITY);

    public static int weaponSpawnLevel = 1;

    public static int GetBiggestWeaponId()
    {
        return slots.Max((Slot s) => s == null ? -1 : s.weapon == null ? -1 : s.weapon.id);
    }

    static Inventory()
    {
        for (int i = 0; i < SLOT_QUANTITY; i++)
        {
            slots.Add(new Slot());
        }
    }

    public static int[] GetCoresSaveInfo()
    {
        List<int> info = new List<int>();
        slots.ForEach((Slot s) => { info.AddRange(s.GetCoresSaveInfo()); });
        return info.ToArray();
    }

    public static void ApplySaveInfo(int[] info)
    {
        List<int> coresInfo = new List<int>(info);
        slots.ForEach((Slot s) =>
        {
            List<int> currentSlotInfo = coresInfo.GetRange(0, 6);
            coresInfo.RemoveRange(0, 6);
            s.ApplyInfo(currentSlotInfo);
        });
    }

    public static WeaponObject GetWeapon(int weaponIndex)
    {
        return slots[weaponIndex].weapon;
    }
}
