using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class InventorySystem : MonoBehaviour
{
    private static InventorySystem instance;

    public GameObject inventoryGrid;
    public static InventorySystem Instance { get => instance; }
    public MoneyPocket Pocket;

    public GameObject movableWeapon;     

    public List<WeaponObject> weaponObjects = new List<WeaponObject>();

    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> weaponsInSlots = new List<GameObject>();

    [HideInInspector]
    public static int lastMovedSlotNumber;
    [HideInInspector]
    public static int lastDropedSlotNumber;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        populateLists();
    }

    public void populateLists()
    {
        slots = new List<GameObject>(); 
        weaponsInSlots = new List<GameObject>();
        foreach (Transform slot in inventoryGrid.transform)
        {
            slots.Add(slot.gameObject);
            weaponsInSlots.Add(slot.transform.Find("Weapon").gameObject);
        }
    }

}

//[CustomEditor(typeof(InventorySystem))]
//public class FillUpInventoryLists : Editor
//{
//    //List<GameObject> slots;
//    //List<GameObject> weaponsInSlots;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        //InventorySystem inventorySystem = (InventorySystem)target;
//        //GameObject inventoryGrid =  GameObject.Find("Inventory Grid");
//        InventorySystem inventorySystem = (InventorySystem)target;
//        if (GUILayout.Button("Fill Up Lists")){
//            inventorySystem.populateLists();
//            //slots = new List<GameObject>();
//            //weaponsInSlots = new List<GameObject>();

//            //foreach (Transform slot in inventoryGrid.transform)
//            //{
//            //    slots.Add(slot.gameObject);
//            //    weaponsInSlots.Add(slot.transform.GetChild(0).gameObject);
//            //}

//            //inventorySystem.slots = new List<GameObject>(slots);
//            //inventorySystem.weaponsInSlots = weaponsInSlots;
//        }
//    }
//}