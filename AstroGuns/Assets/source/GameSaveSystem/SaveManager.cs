using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SaveManager Instance { get => instance; }

    private SaveManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadGame();
        StartCoroutine(SaveCoroutine());
    }

    private IEnumerator SaveCoroutine()
    {
        while (true)
        {
            //Debug.Log("SAVE0");
            SaveGame();

            yield return new WaitForSeconds(5.0f);
        }
    }

    public void SaveGame()
    {
        //Debug.Log("SAVE1");
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();

        if(data != null)
        {
            Inventory.weaponSpawnLevel = data.weaponSpawnLevel;

            for(int i = 0; i < Inventory.SLOT_QUANTITY; i++)
            {
                WeaponSpawner.setWeaponData(i, data.weaponsId[i]);
                WeaponSpawner.resetWeaponView(i);
            }
        }
    }
}
