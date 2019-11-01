using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public const int ROWS = 6;
    public const int COLS = 4;
    public const int SLOT_QUANTITY = ROWS * COLS;

    public static List<Slot> Slots = new List<Slot>(SLOT_QUANTITY);

    public static int weaponSpawnLevel = 0;

    static Inventory()
    {
        for (int i = 0; i < SLOT_QUANTITY; i++)
        {
            Slots.Add(new Slot());
        }
    }
}
