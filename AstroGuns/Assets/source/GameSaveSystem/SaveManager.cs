using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private SaveManager instance;
    public SaveManager Instance { get => instance; }

    public bool ResetGame = false;

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
        LoadGame(); // to jest w starcie, bo sloty i bronie wpisują się na listę (Inventory System) w awake, a wczytujemy właśnie do tych slotów dane
        StartCoroutine(SaveCoroutine()); // mejbi później to przeniesiemy, żeby zapisywało co każdy resp/merge
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
        if(ResetGame)
            data.Reset();
        PlayerData.ApplyPlayerData(data);
    }
}
