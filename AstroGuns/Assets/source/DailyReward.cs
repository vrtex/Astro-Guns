using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyReward : MonoBehaviour
{
	private static DailyReward	instance;
	public	static DailyReward	Instance { get => instance; }

	public GameObject           dailyButton	= null;
	public GameObject           dailyRows	= null;
	private List<GameObject>    dailyCovers	= new List<GameObject>();

	void Awake()
    {
		if(instance == null) instance = this;
	}

	void Start()
	{
		DateTimeSystem.Instance.afterGetTime.AddListener(ShowRewardButton);
	}

    void Update()
    {
        
    }

	public void ShowRewardButton()
	{
		if(DateTimeSystem.Instance.GetCurrentDateNow() > DateTimeSystem.Instance.lastTimeDailyReward)
		{
			dailyButton.SetActive(true);
		}
	}

	public void ShowCurrentReward()
	{

	}

	public void GetCurrentReward()
	{
		++DateTimeSystem.Instance.lastDailyReward;
		dailyButton.SetActive(false);
	}
}
