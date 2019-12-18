using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class StatisticsManager : MonoBehaviour
{
    private static StatisticsManager instance;
    public static StatisticsManager Instance { get => instance; }

    public bool isRunFirstTime = false;
    public int merge = 0;

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
    
    public void increaseMerge()
    {
        merge++;
    }

    public void runGame()
    {
        //if(isRunFirstTime == false)
        //{
            isRunFirstTime = true;
            AchievementManager.Instance.RevealAchievement(EM_GameServicesConstants.Achievement_Run_the_game);
            AchievementManager.Instance.UnlockAchievement(EM_GameServicesConstants.Achievement_Run_the_game);
        //}
    }
}
