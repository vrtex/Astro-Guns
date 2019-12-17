using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager instance;
    public static AchievementManager Instance { get => instance; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ShowAchievementsUI()
    {
        // Check for initialization before showing achievements UI
        if (GameServices.IsInitialized())
        {
            GameServices.ShowAchievementsUI();
        }
        else
        {
            #if UNITY_ANDROID
                GameServices.Init();    // start a new initialization process
            #elif UNITY_IOS
                Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
            #endif
        }
    }
}
