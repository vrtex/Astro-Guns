using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class DateTimeSystem : MonoBehaviour
{
	private static DateTimeSystem	instance;
	public static DateTimeSystem	Instance { get => instance; }

	//zmienne do połączenia
	private string              url                     = "http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php";
	private string              timeData                = "";
	private string              currentTime             = "";
	private string              currentDate             = "";
	private bool                timeIsGet               = false;

	public  int                 lastDailyReward         = -1;
	public  int                 lastTimeDailyReward     = 0;

	public  long				lastTimeAfterOffGame    = -1;

	public UnityEvent			afterGetTime            = new UnityEvent();

	void Awake()
	{
		if(instance == null) instance = this;
	}

	void Start()
    {
		StartCoroutine("GetTime");
	}

    void Update()
    {
		StartCoroutine("GetTimeInterval");
	}

	public IEnumerator GetTime()
	{
		WWW www = new WWW (url);
		yield return www;
		if(www.error != null)
		{
			Debug.Log("No internet connection. Can't get date");
		}
		else
		{
			timeData = www.text;
			string[] words = timeData.Split('/');

			currentDate = words[0];
			currentTime = words[1];

			timeIsGet = true;
			afterGetTime.Invoke();
		}
	}

	public IEnumerator GetTimeInterval()
	{
		yield return new WaitForSeconds(1);
		lastTimeAfterOffGame = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}

	public int GetCurrentDateNow()
	{
		if(!timeIsGet) return -1;

		string[] words = currentDate.Split('-');
		int x = int.Parse(words[0] + words[1] + words[2]);
		return x;
	}

	public string GetCurrentTimeNow()
	{
		return currentTime;
	}
}
